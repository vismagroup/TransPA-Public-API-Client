using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Headers;
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

    public DatalonApiClient(IHttpClientFactory httpClientFactory, ILogger<DatalonApiClient> log)
    {
        _client = httpClientFactory.CreateClient();
        _log = log;
    }

    public async Task SetAuthenticationHeader(string datalonRefreshToken) // Corresponds to tenant_id in TransPA
    {
        var apimSubscriptionKey = Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.OcpApimSubscriptionKey) ?? "";
        if (String.IsNullOrEmpty(apimSubscriptionKey))
        {
            _log.LogError("Ocp-Apim-Subscription-Key is not set");
            throw new Exception("Ocp-Apim-Subscription-Key is not set");
        }

        if (!_client.DefaultRequestHeaders.Contains("Ocp-Apim-Subscription-Key"))
        {
            _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apimSubscriptionKey);
        }

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", await GetBearerTokenAsync(datalonRefreshToken));
    }

    public async Task<string> GetBearerTokenAsync(string refreshToken)
    {
        var datalonApi = Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.DatalonOauthApiHost) ?? "";
        if (String.IsNullOrEmpty(datalonApi))
        {
            _log.LogError("datalonApiHost is missing");
            throw new Exception("datalonApiHost is missing");
        }

        var body = new BearerTokenRequestBody(refreshToken);
        var request = new HttpRequestMessage(HttpMethod.Post, $"{datalonApi}/api/LoginRefreshToken");

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
                $"{Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.DatalonApiHost)}/api/input/salary");
        var jsonBody = await responseMessage.Content.ReadAsStringAsync();
        var root = JsonConvert.DeserializeObject<SalaryRootResponseBody>(jsonBody);

        return root.employerId;
    }

    public async Task<string> GetEmployeeIdAsync(string employeeNumber, string employerId)
    {
        var responseMessage =
            await _client.GetAsync(
                $"{Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.DatalonApiHost)}/api/input/salary/{employerId}/employees");
        var jsonBody = await responseMessage.Content.ReadAsStringAsync();
        var employees = JsonConvert.DeserializeObject<ResourceCollectionBody<EmployeeResponseBody>>(jsonBody);

        var datalonNonUniqueEmployeeNumber = employeeNumber.Substring(2);
        var datalonSalaryPeriodCode = employeeNumber.Substring(0, 2);

        return employees.collection
            .First(e => e.employeeNumber.Equals(datalonNonUniqueEmployeeNumber) && e.salaryPeriodCode.Equals(datalonSalaryPeriodCode))
            .employeeId;
    }

    public async Task<ICollection<Form>> GetFormsForEmployee(Salary salary, string employerId, string employeeId)
    {
        var responseMessage =
            await _client.GetAsync(
                $"{Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.DatalonApiHost)}/api/input/salary/{employerId}/forms?from={salary.StartDate}&to={salary.EndDate}&pageSize=5000");

        var jsonBody = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<ResourceCollectionBodyExtended<Form>>(jsonBody);

        if (response.totalCount == response.pageSize)
        {
            _log.LogError(
                $"Reading committed forms return more than we built support for ({response.totalCount}). Value need to be adjusted, or read next value");
        }

        var forms = response.collection;
        if (!forms.Any())
        {
            return forms;
        }

        return forms.Where(f => f.entries.First().employeeId.Equals(employeeId) && f.state.Equals(FormStatusCommitted)).ToList();
    }

    public async Task<bool> ArchiveForm(string formId, string employerId)
    {
        var request = new HttpRequestMessage(HttpMethod.Post,
            $"{Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.DatalonApiHost)}/api/input/salary/{employerId}/forms/archive/{formId}");

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
            $"{Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.DatalonApiHost)}/api/input/salary/{employerId}/forms/commit");
        var requestBody = new ResourceCollectionBody<Form>()
        {
            collection = new List<Form>()
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

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
    internal class EmployeeResponseBody
    {
        public string employeeId { get; set; } = null!;
        public string employeeNumber { get; set; } = null!;
        public string salaryPeriodCode { get; set; } = null!;
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