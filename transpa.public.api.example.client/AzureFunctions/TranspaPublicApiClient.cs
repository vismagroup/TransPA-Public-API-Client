using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using transpa.api.generated.Model;
using TransPA.OpenSource.Constants;

namespace TransPA.OpenSource;

public interface IPublicApiClient
{
    Task SetAuthenticationHeaderAsync(string tenantId);
    Task<Salary> GetSalaryAsync(SalaryCreated salaryCreated);
    Task<Employee> GetEmployeeAsync(string salaryEmployeeId, string resourceUrl);

    Task<bool> setExportFailed(string resourceUrl, SalaryExportFailed salaryExportFailed);
    Task<bool> setExportSuccess(string resourceUrl);

}

public class PublicApiClient : IPublicApiClient
{
    private readonly HttpClient _client;
    private readonly ILogger<PublicApiClient> _log;
    
    private readonly string _vismaConnectHost;
    private readonly string _apimSubscriptionKey;

    public PublicApiClient(IHttpClientFactory httpClientFactory, ILogger<PublicApiClient> log)
    {
        _client = httpClientFactory.CreateClient();
        _log = log;
        
        _vismaConnectHost = Environment.GetEnvironmentVariable(TranspaPublicApiConfigurationNameConstants.VismaConnectHost) ?? "";
        if (String.IsNullOrEmpty(_vismaConnectHost))
        {
            _log.LogError("ConnectHost is not properly set up");
        }
        
        _apimSubscriptionKey = Environment.GetEnvironmentVariable(TranspaPublicApiConfigurationNameConstants.OcpApimSubscriptionKey) ?? "";
        if (String.IsNullOrEmpty(_apimSubscriptionKey))
        {
            _log.LogError("Ocp-Apim-Subscription-Key is not set");
        }
    }

    public async Task SetAuthenticationHeaderAsync(string tenantId)
    {
        _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apimSubscriptionKey);

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", (await GetBearerTokenAsync(tenantId)).access_token);
    }

    private async Task<BearerTokenOkResponseBody> GetBearerTokenAsync(string tenantId)
    {
        var clientId = Environment.GetEnvironmentVariable(TranspaPublicApiConfigurationNameConstants.PublicAPIClientId) ?? "";
        var clientSecret = Environment.GetEnvironmentVariable(TranspaPublicApiConfigurationNameConstants.PublicAPIClientSecret) ?? "";

        if (String.IsNullOrEmpty(clientId) || String.IsNullOrEmpty(clientSecret))
        {
            _log.LogError("Client credentials is missing");
            throw new Exception("Client credentials is missing ");
        }

        var body =
            $"grant_type=client_credentials&transpaapi:salaries:read+transpaapi:api+transpaapi:employees:read&client_id={clientId}&client_secret={clientSecret}&tenant_id={tenantId}";

        var request = new HttpRequestMessage(HttpMethod.Post, GetBearerTokenUrl());
        request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");

        request.Content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");

        var response = await _client.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var jsonBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BearerTokenOkResponseBody>(jsonBody);
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var jsonBody = await response.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<BearerTokenBadRequestResponseBody>(jsonBody);
            _log.LogError($"Received and BadRequest response when trying to read bearer token. Error code: {responseBody.error}");
        }
        else
        {
            _log.LogError("Unknown error");
        }

        throw new Exception("Failed reading bearer token from Visma Connect");
    }

    private string GetBearerTokenUrl()
    {
        return $"https://{_vismaConnectHost}/connect/token";
    }

    public async Task<Salary> GetSalaryAsync(SalaryCreated salaryCreated)
    {
        var httpResponseMessage = await _client.GetAsync(salaryCreated.ResourceUrl);

        switch (httpResponseMessage.StatusCode)
        {
            case HttpStatusCode.OK:
                return await httpResponseMessage.Content.ReadAsAsync<Salary>();
            case HttpStatusCode.NotFound:
                _log.LogError("Salary doesn't exist");
                break;
            case HttpStatusCode.Unauthorized:
                var jsonAsString = await httpResponseMessage.Content.ReadAsStringAsync();
                _log.LogError($"Unauthorized when reading salary. Response body {jsonAsString}");
                break;
            default:
                _log.LogError($"An error occured when reading the salary from TransPA. HttpStatusCode: {httpResponseMessage.StatusCode}");
                break;
        }

        throw new Exception("Failed to read salary");
    }

    public async Task<Employee> GetEmployeeAsync(string employeeId, string host)
    {
        var employeeResourceUrl = $"https://{host}/publicApi/v1/employees/{employeeId}";
        var httpResponseMessage = await _client.GetAsync(employeeResourceUrl);

        switch (httpResponseMessage.StatusCode)
        {
            case HttpStatusCode.OK:
                return await httpResponseMessage.Content.ReadAsAsync<Employee>();
            case HttpStatusCode.NotFound:
                _log.LogError("Employee doesn't exist");
                break;
            case HttpStatusCode.Unauthorized:
                var jsonAsString = await httpResponseMessage.Content.ReadAsStringAsync();
                _log.LogError($"Unauthorized when reading employee. Response body {jsonAsString}");
                break;
            default:
                _log.LogError($"An error occured when reading the employee from TransPA. HttpStatusCode: {httpResponseMessage.StatusCode}");
                break;
        }

        throw new Exception("Failed to read employee");
    }

    public async Task<bool> setExportFailed(string resourceUrl, SalaryExportFailed salaryExportFailed)
    {
        resourceUrl = string.Concat(resourceUrl, "/setExportFailed");

        var stringContent = new StringContent(JsonConvert.SerializeObject(salaryExportFailed), Encoding.UTF8, "application/json");
        var httpResponseMessage = await _client.PostAsync(resourceUrl, stringContent);
        switch (httpResponseMessage.StatusCode)
        {
            case HttpStatusCode.Forbidden:
            case HttpStatusCode.BadRequest:
            case HttpStatusCode.Conflict:
                _log.LogError(string.Format("{0} HttpStatusCode:{1}", httpResponseMessage.Content.ReadAsStringAsync().Result, httpResponseMessage.StatusCode));
                break;
            case HttpStatusCode.NoContent:
                _log.LogInformation("Export set as failed.");
                return true;
            default:
                _log.LogError($"An error occured when setting the export as failed. HttpStatusCode: {httpResponseMessage.StatusCode}");
                break;
        }

        throw new Exception("Failed to set export as failed!");
    }

    public async Task<bool> setExportSuccess(string resourceUrl)
    {
        resourceUrl = string.Concat(resourceUrl, "/setExportSuccess");

        var httpResponseMessage = await _client.PostAsync(resourceUrl, null);
        switch (httpResponseMessage.StatusCode)
        {
            case HttpStatusCode.Forbidden:
            case HttpStatusCode.BadRequest:
            case HttpStatusCode.Conflict:
                _log.LogError(string.Format("{0} HttpStatusCode:{1}", httpResponseMessage.Content.ReadAsStringAsync().Result, httpResponseMessage.StatusCode));
                break;
            case HttpStatusCode.NoContent:
                _log.LogInformation("Export set as successful.");
                return true;
            default:
                _log.LogError($"An error occured when setting the export as successful. HttpStatusCode: {httpResponseMessage.StatusCode}");
                break;
        }

        throw new Exception("Failed to set export as successful!");
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
    private class BearerTokenOkResponseBody
    {
        public string access_token { get; set; } = null!;
        public int expires_in { get; set; }
        public string token_type { get; set; } = null!;
        public string scope { get; set; } = null!;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
    private class BearerTokenBadRequestResponseBody
    {
        public BearerTokenBadRequestResponseBody(string error)
        {
            this.error = error;
        }

        public string error { get; set; }
    }
}