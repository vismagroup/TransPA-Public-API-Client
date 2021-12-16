using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using transpa.api.generated.Model;

namespace TransPA.OpenSource.Functions
{
    public class TranspaToDatalon
    {
        private readonly PublicApiClient _publicApiClient;
        public TranspaToDatalon(PublicApiClient publicApiClient)
        {
            _publicApiClient = publicApiClient;
        }
        
        [FunctionName("DataLon")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "integrations/datalon")]
            [FromBody] string body, HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var salaryCreated = JsonConvert.DeserializeObject<SalaryCreated>(body);

            await _publicApiClient.SetAuthenticationHeader(salaryCreated.TenantId);
            var employee = await _publicApiClient.GetEmployeeAsync(salaryCreated);

            return new OkObjectResult(employee.EmployeeNumber);
        }
    }
}
