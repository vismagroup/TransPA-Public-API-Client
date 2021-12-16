using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TransPA.OpenSource.Constants;

namespace TransPA.OpenSource;

public class DatalonApiClient
{
    private readonly HttpClient _client;
    private readonly ILogger<PublicApiClient> _log;

    public DatalonApiClient(IHttpClientFactory httpClientFactory, ILogger<PublicApiClient> log)
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

        _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apimSubscriptionKey);

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", await GetBearerTokenAsync(datalonRefreshToken));
    }

    public async Task<string> GetBearerTokenAsync(string refreshToken)
    {
        var datalonApi = Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.DatalonApiHost) ?? "";
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


    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private class SalaryRoot
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