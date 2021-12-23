using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using transpa.api.generated.Model;
using TransPA.OpenSource.Constants;
using TransPA.OpenSource.External.Datalon.Model;

namespace TransPA.OpenSource;

public interface IDatalonApiClient
{
    Task SetAuthenticationHeader(string datalonRefreshToken);

    Task<string> GetEmployeeIdAsync(string employeeNumber, string employerId);
    Task<ICollection<Form>> GetFormsForEmployee(Salary salary, string employerId, string employeeId);
    Task<bool> ArchiveForm(string formId, string employerId);
    Task<bool> CommitForm(Form form, string employerId);
    Task<string> GetEmployerId();
}

public class DatalonApiClient : IDatalonApiClient
{
    public const string FormStatusCommitted = "committed";

    private readonly HttpClient _client;
    private readonly ILogger<DatalonApiClient> _log;
    private readonly string _datalonApiHost;
    private readonly string _datalonOauthApiHost;
    private readonly string _apimSubscriptionKey;
    private const string DateFormat = "dd/MM/yyyy";

    public DatalonApiClient(IHttpClientFactory httpClientFactory, ILogger<DatalonApiClient> log)
    {
        _client = httpClientFactory.CreateClient();
        _log = log;
        _datalonApiHost =  Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.DatalonApiHost) ?? "https://preprod-dataloen-api.bluegarden.dk";

        if (String.IsNullOrEmpty(_datalonApiHost))
        {
            _log.LogError("DataLonApiHost is not set");
        }
        
        _datalonOauthApiHost = Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.DatalonOauthApiHost) ?? "";
        if (String.IsNullOrEmpty(_datalonOauthApiHost))
        {
            _log.LogError("datalonApiHost is not set");
        }
        
        _apimSubscriptionKey = Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.OcpApimSubscriptionKey) ?? "";
        if (String.IsNullOrEmpty(_apimSubscriptionKey))
        {
            _log.LogError("Ocp-Apim-Subscription-Key is not set");
        }
    }

    public async Task SetAuthenticationHeader(string datalonRefreshToken) // Corresponds to tenant_id in TransPA
    {
        if (!_client.DefaultRequestHeaders.Contains("Ocp-Apim-Subscription-Key"))
        {
            _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apimSubscriptionKey);
        }

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", await GetBearerTokenAsync(datalonRefreshToken));
    }

    private async Task<string> GetBearerTokenAsync(string refreshToken)
    {
        var body = new BearerTokenRequestBody(refreshToken);
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_datalonOauthApiHost}/api/LoginRefreshToken");

        request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

        var response = await _client.SendAsync(request);

        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
                return JsonConvert.DeserializeObject<BearerTokenResponseBody>(await response.Content.ReadAsStringAsync()).accessToken;
            default:
                _log.LogError("Failed reading JWT token for DataLon");
                break;
        }

        throw new Exception("Failed reading JWT token for DataLon");
    }
    
    public async Task<string> GetEmployerId()
    {
        var responseMessage =
            await _client.GetAsync(
                $"{_datalonApiHost}/api/input/salary");
        var jsonBody = await responseMessage.Content.ReadAsStringAsync();
        var root = JsonConvert.DeserializeObject<SalaryRootResponseBody>(jsonBody);

        return root.employerId;
    }

    public async Task<string> GetEmployeeIdAsync(string employeeNumber, string employerId)
    {
        var responseMessage =
            await _client.GetAsync(
                $"{_datalonApiHost}/api/input/salary/{employerId}/employees");
        var jsonBody = await responseMessage.Content.ReadAsStringAsync();
        var employees = JsonConvert.DeserializeObject<ResourceCollectionBody<EmployeeResponseBody>>(jsonBody);

        var datalonNonUniqueEmployeeNumber = employeeNumber.Substring(2);
        var datalonSalaryPeriodCode = employeeNumber.Substring(0, 2);

        return employees.Collection
            .First(e => e.EmployeeNumber.Equals(datalonNonUniqueEmployeeNumber) && e.SalaryPeriodCode.Equals(datalonSalaryPeriodCode))
            .EmployeeId;
    }

    public async Task<ICollection<Form>> GetFormsForEmployee(Salary salary, string employerId, string employeeId)
    {
        var requestUri = $"{_datalonApiHost}/api/input/salary/{employerId}/forms?from={ salary.StartDate.ToString(DateFormat)}&to={salary.EndDate.ToString(DateFormat)}&pageSize=5000";
        _log.LogDebug($"GetForms uri {requestUri}");
        var responseMessage =
            await _client.GetAsync(
                requestUri);

        var jsonBody = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<ResourceCollectionBodyExtended<Form>>(jsonBody);

        if (response.TotalCount == response.PageSize)
        {
            _log.LogError(
                $"Reading committed forms return more than we built support for ({response.TotalCount}). Value need to be adjusted, or read next value");
        }

        var forms = response.Collection;
        _log.LogInformation($"There are {forms.Count} forms to be checked");
        if (!forms.Any())
        {
            return forms;
        }

        return forms.Where(f => f.Entries.First().EmployeeId.Equals(employeeId) && f.State.Equals(FormStatusCommitted)).ToList();
    }

    public async Task<bool> ArchiveForm(string formId, string employerId)
    {
        var request = new HttpRequestMessage(HttpMethod.Post,
            $"{_datalonApiHost}/api/input/salary/{employerId}/forms/archive/{formId}");

        var responseMessage = await _client.SendAsync(request);
        if (!responseMessage.IsSuccessStatusCode)
        {
            _log.LogError($"Unknown error happened when trying to archive form {formId} for employer {employerId}. Https status code {responseMessage.StatusCode}");
        }

        return responseMessage.IsSuccessStatusCode;
    }

    public async Task<bool> CommitForm(Form form, string employerId)
    {
        var request = new HttpRequestMessage(HttpMethod.Post,
            $"{_datalonApiHost}/api/input/salary/{employerId}/forms/commit");
        var requestBody = new ResourceCollectionBody<Form>()
        {
            Collection = new List<Form>()
            {
                form
            }
        };
        
        request.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        var responseMessage = await _client.SendAsync(request);

        switch (responseMessage.StatusCode)
        {
            case HttpStatusCode.OK:
                return true;
            case HttpStatusCode.BadRequest:
                _log.LogError("Bad request - something went wrong"); 
                // TODO: In a later ticket we need to report back an error here depending on the error, hopefully with indication or resolution 
                throw new Exception("BadRequest error");
            default:
                _log.LogError($"Unspecified - {responseMessage.StatusCode} - Something went wrong"); 
                // TODO: In a later ticket we need to report back an unhandled error here 
                throw new Exception("Other error");
        }
    }

    internal class EmployeeResponseBody
    {
        public EmployeeResponseBody(string employeeId = default!, string employeeNumber =default!, string salaryPeriodCode = default!)
        {
            EmployeeId = employeeId;
            EmployeeNumber = employeeNumber;
            SalaryPeriodCode = salaryPeriodCode;
        }

        [DataMember(Name = "employeeId", EmitDefaultValue = false)]
        public string EmployeeId { get; set; }
        [DataMember(Name = "employeeNumber", EmitDefaultValue = false)]
        public string EmployeeNumber { get; set; }
        [DataMember(Name = "salaryPeriodCode", EmitDefaultValue = false)]
        public string SalaryPeriodCode { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "UnusedType.Local")]
    private class SalaryRootResponseBody
    {
        public string employerId { get; set; } = null!;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
    private class BearerTokenResponseBody
    {
        public string accessToken { get; set; } = null!;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    private class BearerTokenRequestBody
    {
        public BearerTokenRequestBody(string refreshToken)
        {
            this.refreshToken = refreshToken;
        }

        public string refreshToken;
    }
}