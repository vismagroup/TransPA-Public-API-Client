using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using transpa.api.generated.Model;
using TransPA.OpenSource.Constants;

namespace TransPA.OpenSource.Functions
{
    public class TranspaToDatalon
    {
        private readonly PublicApiClient _publicApiClient;
        private readonly DatalonApiClient _datalonApiClient;
        public TranspaToDatalon(PublicApiClient publicApiClient, DatalonApiClient datalonApiClient)
        {
            _publicApiClient = publicApiClient;
            _datalonApiClient = datalonApiClient;
        }
        
        [FunctionName("DataLon")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "integrations/datalon")]
            [FromBody] string body, HttpRequest req, ILogger log)
        {
            /*
             * Read data from TransPA Public API.
             */
            var salaryCreated = JsonConvert.DeserializeObject<SalaryCreated>(body);

            await _publicApiClient.SetAuthenticationHeader(salaryCreated.TenantId); // TODO: Have to be reworked to be able to handle different tenants (singleton problem)

            var salary = await _publicApiClient.GetSalaryAsync(salaryCreated);

            var uri = new Uri(salaryCreated.ResourceUrl);

            var employeeResourceUrl = $"https://{uri.Host}/publicApi/v1/employees/{salary.EmployeeId}";
            var employee = await _publicApiClient.GetEmployeeAsync(employeeResourceUrl);
            
            /*
             * Integration specific
             */
            var environmentVariable = Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.RefreshToken) ?? "";
            if (String.IsNullOrEmpty(environmentVariable))
            {
                log.LogError("Missing DataLon refreshToken");
                throw new Exception("Missing DataLon refreshToken");
            }

            await _datalonApiClient.SetAuthenticationHeader(environmentVariable); // TODO: Have to be reworked to be able to handle different tenants (singleton problem)
            

            return new OkObjectResult(employee.EmployeeNumber); // Remark: TransPA will not do anything with the return code here. It's essential that you report the status via the API.
        }
    }
}
