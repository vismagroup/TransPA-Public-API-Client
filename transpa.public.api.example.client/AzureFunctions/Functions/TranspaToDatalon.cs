using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using transpa.api.generated.Model;
using TransPA.OpenSource.Constants;
using TransPA.OpenSource.External.Datalon;
using TransPA.OpenSource.External.Datalon.Model;

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

        internal const string FailedUnspecified = "failedUnspecified";

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
             * We recommend that you protect your webhook with some basic authentication that is setup'ed via custom headers when
             * registering the webhook.
             *
             * The webhook does not receive personal data in the request body, but will have access to both systems and could be a potential attack surface.
             */
            var secret = Environment.GetEnvironmentVariable(TranspaPublicApiConfigurationNameConstants.WebhookSecret);
            if (String.IsNullOrEmpty(secret) || !req.Headers.TryGetValue("Authorization", out var authorizationHeaderValue) || !secret.Equals(authorizationHeaderValue))
            {
                log.LogError("No authorization header was provided");
                return new StatusCodeResult(403);
            }
            
            /*
             * Read data from TransPA Public API.
             */
            var salaryCreated = JsonConvert.DeserializeObject<SalaryCreated>(body);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (salaryCreated == null)
            {
                log.LogError("Failed to parse the json body");
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
                var salaryExportFailed = new SalaryExportFailed(employeeValidationResult.Errors.Select(x => x.ErrorCode).FirstOrDefault(), string.Empty);
                await _publicApiClient.SetExportFailedAsync(uri.Host, salaryExportFailed, salary.Id);

                return _httpObjectResultHelper.GetBadRequestResult("EmployeeNumber is not properly configured in TransPA");
            }

            var salaryValidationResult = await _salaryValidator.ValidateAsync(salary);
            if (!salaryValidationResult.IsValid)
            {
                var salaryExportFailed = new SalaryExportFailed(salaryValidationResult.Errors.Select(x => x.ErrorCode).FirstOrDefault(), string.Empty);
                await _publicApiClient.SetExportFailedAsync(uri.Host, salaryExportFailed, salary.Id);

                return _httpObjectResultHelper.GetBadRequestResult(salaryValidationResult.Errors.First().ErrorMessage);
            }

            var employeeNumberAsString = employee.EmployeeNumber!.Value.ToString();

            if (!req.Headers.TryGetValue(DatalonApiConfigurationNameConstants.RefreshToken, out var datalonRefreshToken))
            { // If you use a access token with tenant this could be replaced with tenant id, or you may do this mapping on your end
                log.LogError("Refresh token is not provided");
                var salaryExportFailed = new SalaryExportFailed(FailedUnspecified, "Refresh token is not configured");
                await _publicApiClient.SetExportFailedAsync(uri.Host, salaryExportFailed, salary.Id);
            }

            try
            {
                await _datalonApiClient
                    .SetAuthenticationHeader(
                        datalonRefreshToken); // TODO: Have to be reworked to be able to handle different tenants/refresh tokens (singleton problem) - TPA-2658

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
                var salaryExportFailed = new SalaryExportFailed(FailedUnspecified);
                await _publicApiClient.SetExportFailedAsync(uri.Host, salaryExportFailed, salary.Id);
                log.LogError("Failed in an unexpected way.", ex);
                throw;
            }

            await _publicApiClient.SetExportSuccessAsync(uri.Host, salary.Id);

            return new OkObjectResult("");
        }
    }
}