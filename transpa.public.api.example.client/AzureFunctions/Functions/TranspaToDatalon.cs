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
        private readonly IPublicApiClient _publicApiClient;
        private readonly IDatalonApiClient _datalonApiClient;
        public TranspaToDatalon(IPublicApiClient publicApiClient, IDatalonApiClient datalonApiClient)
        {
            _publicApiClient = publicApiClient;
            _datalonApiClient = datalonApiClient;
        }
        
        [FunctionName("DataLon")]
        public async Task<IActionResult> ExportSalaryFromTranspaToDatalon(
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
            
            if (employee.EmployeeNumber == null)
            {
                log.LogWarning("EmployeeNumber is not set");
                // TODO: Report back handled error here in a later ticket
                return new BadRequestResult();
            }

            /*
             * Integration specific
             */
            var employeeNumberAsString = employee.EmployeeNumber.Value.ToString();
            if (!employeeNumberAsString.Length.Equals(6))
            {
                log.LogWarning("EmployeeNumber is in an incorrect format");
                // TODO: Report back handled error here in a later ticket
                return new BadRequestResult();
            }

            var environmentVariable = Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.RefreshToken) ?? "";
            if (String.IsNullOrEmpty(environmentVariable))
            {
                log.LogError("Missing DataLon refreshToken");
                throw new Exception("Missing DataLon refreshToken");
            }

            await _datalonApiClient.SetAuthenticationHeader(environmentVariable); // TODO: Have to be reworked to be able to handle different tenants/refresh tokens (singleton problem)

            var datalonEmployerId = "62000059"; // TODO: Read this from API, or retrieve it from the JWT token
            var datalonEmployeeId = await _datalonApiClient.GetEmployeeIdAsync(employeeNumberAsString, datalonEmployerId);

            var existingFormsForEmployee = await _datalonApiClient.GetFormsForEmployee(salary, datalonEmployerId, datalonEmployeeId);

            return new OkObjectResult(existingFormsForEmployee.Count); // Remark: TransPA will not do anything with the return code here. It's essential that you report the status via the API.
        }
    }
}
