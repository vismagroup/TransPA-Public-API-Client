using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using transpa.api.generated.Model;

namespace TransPA.OpenSource;

public class PublicApiClient
{
    private readonly HttpClient _client;
    private readonly ILogger<PublicApiClient> _log;

    public PublicApiClient(IHttpClientFactory httpClientFactory, ILogger<PublicApiClient> log)
    {
        _client = httpClientFactory.CreateClient();
        _log = log;
    }

    public async Task SetAuthenticationHeader(string tenantId)
    {
        var apimSubscriptionKey = Environment.GetEnvironmentVariable(ConfigurationNameConstants.OcpApimSubscriptionKey) ?? "";
        if (String.IsNullOrEmpty(apimSubscriptionKey))
        {
            _log.LogError("Ocp-Apim-Subscription-Key is not set");
            throw new Exception("Ocp-Apim-Subscription-Key is not set");
        }
        
        _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apimSubscriptionKey);

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", (await GetBearerTokenAsync(tenantId)).access_token);
        
    }

    private async Task<BearerTokenOkResponseBody> GetBearerTokenAsync(string tenantId)
    {
        var clientId = Environment.GetEnvironmentVariable(ConfigurationNameConstants.PublicAPIClientId) ?? "";
        var clientSecret = Environment.GetEnvironmentVariable(ConfigurationNameConstants.PublicAPIClientSecret) ?? "";

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
        else if (response.StatusCode != HttpStatusCode.OK)
        {
            _log.LogError("Unknown error");
        }

        throw new Exception("Failed reading bearer token from Visma Connect");
    }

    private string GetBearerTokenUrl()
    {
        var connectHost = Environment.GetEnvironmentVariable(ConfigurationNameConstants.VismaConnectHost);
        if (String.IsNullOrEmpty(connectHost))
        {
            _log.LogError("ConnectHost is not properly set up");
            throw new Exception("Connect host is missing");
        }

        return
            $"https://{Environment.GetEnvironmentVariable(ConfigurationNameConstants.VismaConnectHost)}/connect/token";
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
                _log.LogError($"An error occured when reading the employee from TransPA. HttpStatusCode: {httpResponseMessage.StatusCode}");
                break;
        }

        throw new Exception("Failed to read employee");
    }

    public async Task<Employee> GetEmployeeAsync(string resourceUrl)
    {
        var httpResponseMessage = await _client.GetAsync(resourceUrl);

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

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private class BearerTokenOkResponseBody
    {
        public string access_token { get; set; } = null!;
        public int expires_in { get; set; }
        public string token_type { get; set; } = null!;
        public string scope { get; set; } = null!;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private class BearerTokenBadRequestResponseBody
    {
        public BearerTokenBadRequestResponseBody(string error)
        {
            this.error = error;
        }

        public string error { get; set; }
    }
}