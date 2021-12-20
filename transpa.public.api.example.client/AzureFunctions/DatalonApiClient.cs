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

    public async Task<string> GetEmployeeIdAsync(string employeeNumber, string employerId)
    {
        var responseMessage =
            await _client.GetAsync(
                $"{Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.DatalonApiHost)}/api/input/salary/{employerId}/employees");
        var jsonBody = await responseMessage.Content.ReadAsStringAsync();
        var employees = JsonConvert.DeserializeObject<ResourceCollectionResponseBody<EmployeeResponseBody>>(jsonBody);

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
        var response = JsonConvert.DeserializeObject<ResourceCollectionResponseBodyExtended<Form>>(jsonBody);

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