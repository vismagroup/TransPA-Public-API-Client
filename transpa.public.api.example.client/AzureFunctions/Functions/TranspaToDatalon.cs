using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using transpa.api.generated.Model;
using TransPA.OpenSource.Constants;
using TransPA.OpenSource.External.Datalon;

namespace TransPA.OpenSource.Functions
{
    public class TranspaToDatalon
    {
        private readonly IPublicApiClient _publicApiClient;
        private readonly IDatalonApiClient _datalonApiClient;
        private readonly EmployeeValidator _employeeValidator;
        private readonly SalaryValidator _salaryValidator;
        private readonly SalaryConverter _salaryConverter;
        private readonly HttpObjectResultHelper _httpObjectResultHelper;

        public TranspaToDatalon(IPublicApiClient publicApiClient, IDatalonApiClient datalonApiClient,
            EmployeeValidator employeeValidator, SalaryValidator salaryValidator, SalaryConverter salaryConverter, HttpObjectResultHelper httpObjectResultHelper)
        {
            _publicApiClient = publicApiClient;
            _datalonApiClient = datalonApiClient;
            _employeeValidator = employeeValidator;
            _salaryValidator = salaryValidator;
            _salaryConverter = salaryConverter;
            _httpObjectResultHelper = httpObjectResultHelper;
        }


        [FunctionName("DataLon")]
        public async Task<IActionResult> ExportSalaryFromTranspaToDatalon(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "integrations/datalon")] [FromBody]
            string body, HttpRequest req, ILogger log)
        {
            /*
             * Read data from TransPA Public API.
             */
            var salaryCreated = JsonConvert.DeserializeObject<SalaryCreated>(body);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (salaryCreated == null)
            {
                return _httpObjectResultHelper.GetBadRequestResult("Failed parsing request body");
            }

            await _publicApiClient.SetAuthenticationHeaderAsync(salaryCreated
                .TenantId); // TODO: Have to be reworked to be able to handle different tenants (singleton problem) - TPA-2658

            var salary = await _publicApiClient.GetSalaryAsync(salaryCreated);

            var uri = new Uri(salaryCreated.ResourceUrl);
            var employee = await _publicApiClient.GetEmployeeAsync(salary.EmployeeId, uri.Host);

            /*
             * Integration specific
             */
            var employeeValidationResult = await _employeeValidator.ValidateAsync(employee);
            if (!employeeValidationResult.IsValid)
            {
                // TODO: Report back handled error here in a later ticket
                var sef = new SalaryExportFailed(employeeValidationResult.Errors.Select(x => x.ErrorCode).FirstOrDefault());
                await _publicApiClient.setExportFailedAsync(salaryCreated.ResourceUrl, sef);

                return _httpObjectResultHelper.GetBadRequestResult("EmployeeNumber is not properly configured in TransPA");
            }

            var salaryValidationResult = await _salaryValidator.ValidateAsync(salary);
            if (!salaryValidationResult.IsValid)
            {
                // TODO: Report back handled error here in a later ticket
                var sef = new SalaryExportFailed(salaryValidationResult.Errors.Select(x => x.ErrorCode).FirstOrDefault());
                await _publicApiClient.setExportFailedAsync(salaryCreated.ResourceUrl, sef);

                return _httpObjectResultHelper.GetBadRequestResult(salaryValidationResult.Errors.First().ErrorMessage);
            }

            var employeeNumberAsString = employee.EmployeeNumber!.Value.ToString();

            var environmentVariable = Environment.GetEnvironmentVariable(DatalonApiConfigurationNameConstants.RefreshToken) ?? "";
            if (String.IsNullOrEmpty(environmentVariable))
            {
                log.LogError("Missing DataLon refreshToken");
                throw new Exception("Missing DataLon refreshToken");
            }

            try
            {
                await _datalonApiClient
                    .SetAuthenticationHeader(
                        environmentVariable); // TODO: Have to be reworked to be able to handle different tenants/refresh tokens (singleton problem) - TPA-2658

                var datalonEmployerId = await _datalonApiClient.GetEmployerId();
                var datalonEmployeeId = await _datalonApiClient.GetEmployeeIdAsync(employeeNumberAsString, datalonEmployerId);

                var existingFormsForEmployee = await _datalonApiClient.GetFormsForEmployee(salary, datalonEmployerId, datalonEmployeeId);
                await Parallel.ForEachAsync(existingFormsForEmployee,
                    async (form, cancellationToken) => await _datalonApiClient.ArchiveForm(form.FormId, datalonEmployerId));

                var convert = _salaryConverter.Convert(salary, datalonEmployeeId);
                await _datalonApiClient.CommitForm(convert, datalonEmployerId);
            }
            catch (Exception ex)
            {
                await _publicApiClient.setExportFailedAsync(salaryCreated.ResourceUrl, new SalaryExportFailed { StatusCode = "failedUnspecified" });
                log.LogError(ex.Message);
                throw new Exception(ex.Message);
            }

            return new OkObjectResult(await _publicApiClient.setExportSuccessAsync(salaryCreated.ResourceUrl));

            //return
            //    new OkObjectResult(existingFormsForEmployee
            //        .Count); // Remark: TransPA will not do anything with the return code here. It's essential that you report the status via the API.


        }
    }
}