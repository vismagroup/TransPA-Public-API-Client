using AzureFunctions.Test.TestUtils;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using transpa.api.generated.Model;
using TransPA.OpenSource;
using TransPA.OpenSource.Constants;
using TransPA.OpenSource.External.Datalon;
using TransPA.OpenSource.Functions;

namespace AzureFunctions.Test;

[TestFixture]
public class TranspaToDataLonTest
{
    private TranspaToDatalon _testee = null!;
    private Mock<IPublicApiClient> _publicApiClientMock = null!;
    private Mock<IDatalonApiClient> _datalonApiClientMock = null!;

    private SalaryCreated _salaryCreated = null!;
    private SalaryExportFailed _salaryExportFailed = null!;

    private const string TranspaEmployeeId = "transpa-guid";
    private const string TranspaSalaryGuid = "C86E80CF-1079-453B-AAA4-DC461A40C074";
    internal const string StatusCodeForEmployeeNumberUnknown = "failedEmployeeNumberUnknown";
    internal const string StatusCodeForEmployeeNumberBadFormat = "failedEmployeeNumberBadFormat";
    private const string Host = "example.test";
    private const string TheSecret = "the-secret";

    [SetUp]
    public void SetUp()
    {
        _salaryCreated = new SalaryCreated(tenantId: "aaaa-bbbb-cccc-dddd", title: "A salary resource was created",
            resourceUrl: "https://example.test");
        Environment.SetEnvironmentVariable(TranspaPublicApiConfigurationNameConstants.WebhookSecret, TheSecret);

        _publicApiClientMock = new Mock<IPublicApiClient>();
        _datalonApiClientMock = new Mock<IDatalonApiClient>();
        _testee = new TranspaToDatalon(_publicApiClientMock.Object, _datalonApiClientMock.Object,
            new EmployeeValidator(), new SalaryValidator(), new SalaryConverter(), new HttpObjectResultHelper());
    }

    [Test]
    public async Task EmployeeNumberMissingShouldReturnBadRequest()
    {
        // Arrange
        _salaryExportFailed = new SalaryExportFailed(StatusCodeForEmployeeNumberUnknown, string.Empty);
        var salary = new Salary(employeeId: TranspaEmployeeId, id: TranspaSalaryGuid);
        var employeeNumberMissing = new Employee(employeeNumber: null);
        _publicApiClientMock.Setup(x => x.GetEmployeeAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(employeeNumberMissing);
        _publicApiClientMock.Setup(x => x.GetSalaryAsync(_salaryCreated)).ReturnsAsync(salary);

        // Act
        var result = await _testee.ExportSalaryFromTranspaToDatalon(JsonConvert.SerializeObject(_salaryCreated),
            AzureFunctionTestFactory.CreateHttpRequestWithHeaders(new List<Tuple<string, string>>()
            {
                new("Authorization", TheSecret)
            }), AzureFunctionTestFactory.CreateLogger());

        // Assert
        var response = (ObjectResult) result;
        response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

        _publicApiClientMock.Verify(e => e.SetExportFailedAsync(Host, _salaryExportFailed, TranspaSalaryGuid), Times.Once);
    }

    [Test]
    public async Task EmployeeNumberInABadFormatForDatalonShouldReturnBadRequest()
    {
        // Arrange
        var employeeWithEmployeeNumberWithBadFormat = new Employee(employeeNumber: 24035);
        _publicApiClientMock.Setup(x => x.GetEmployeeAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(employeeWithEmployeeNumberWithBadFormat);
        Salary salary = new Salary(employeeId: TranspaEmployeeId, id: TranspaSalaryGuid);
        _publicApiClientMock.Setup(x => x.GetSalaryAsync(_salaryCreated)).ReturnsAsync(salary);

        _salaryExportFailed = new SalaryExportFailed(StatusCodeForEmployeeNumberBadFormat, string.Empty);

        // Act
        var result = await _testee.ExportSalaryFromTranspaToDatalon(JsonConvert.SerializeObject(_salaryCreated),
            AzureFunctionTestFactory.CreateHttpRequestWithHeaders(new List<Tuple<string, string>>()
            {
                new("Authorization", TheSecret)
            })
            , AzureFunctionTestFactory.CreateLogger());

        // Assert
        var response = (ObjectResult) result;
        response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        _publicApiClientMock.Verify(e => e.SetExportFailedAsync(Host, _salaryExportFailed, TranspaSalaryGuid), Times.Once);
    }

    [TestCase(SalaryValidator.NoRowsExported, SalaryValidator.PayTypeCodeUnknown, "", "")]
    [TestCase(SalaryValidator.WageRowBadFormat, SalaryValidator.PayTypeCodeBadFormat, "12345", "")]
    [TestCase(SalaryValidator.TimeRowBadFormat, SalaryValidator.PayTypeCodeBadFormat, "", "12345")]
    public async Task NoPayCodeSetShouldReturnBadRequest(String expectedResult, string errorCode, string wageRowPayTypeCode,
        string timeRowPayTypeCode)
    {
        // Arrange
        var employee = new Employee(employeeNumber: 240035);
        _publicApiClientMock.Setup(x => x.GetEmployeeAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(employee);
        var salary = new Salary(employeeId: TranspaEmployeeId, id: TranspaSalaryGuid, wageRows: new List<WageRow>()
        {
            new WageRow(wageRowPayTypeCode, new Money(100, "SEK"), 200)
        }, timeRows: new List<TimeRow>()
        {
            new TimeRow(timeRowPayTypeCode)
        });
        _publicApiClientMock.Setup(x => x.GetSalaryAsync(_salaryCreated)).ReturnsAsync(salary);

        _salaryExportFailed = new SalaryExportFailed(errorCode, string.Empty);

        // Act
        var result = await _testee.ExportSalaryFromTranspaToDatalon(JsonConvert.SerializeObject(_salaryCreated),
            AzureFunctionTestFactory.CreateHttpRequestWithHeaders(new List<Tuple<string, string>>()
            {
                new("Authorization", TheSecret)
            }), AzureFunctionTestFactory.CreateLogger());

        // Assert
        var response = (ObjectResult) result;
        response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        response.Value.GetType().Should().Be(typeof(ProblemDetails));
        var details = (ProblemDetails) response.Value;
        details.Detail.Should().Be(expectedResult);
        _publicApiClientMock.Verify(e => e.SetExportFailedAsync(Host, _salaryExportFailed, TranspaSalaryGuid), Times.Once);
    }

    [Test]
    public async Task WrongSecretShouldReturnForbiddenWhenNoHeaderIsSet()
    {
        // Arrange

        // Act
        var result = await _testee.ExportSalaryFromTranspaToDatalon(JsonConvert.SerializeObject(_salaryCreated),
            AzureFunctionTestFactory.CreateHttpRequestWithHeaders(new List<Tuple<string, string>>()), AzureFunctionTestFactory.CreateLogger());

        // Assert
        var response = (StatusCodeResult) result;
        response.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
    }

    [Test]
    public async Task WrongSecretShouldReturnForbiddenWhenTheSecretIsWrong()
    {
        // Arrange
        Environment.SetEnvironmentVariable(TranspaPublicApiConfigurationNameConstants.WebhookSecret, "a secret");
        var result = await _testee.ExportSalaryFromTranspaToDatalon(JsonConvert.SerializeObject(_salaryCreated),
            AzureFunctionTestFactory.CreateHttpRequestWithHeaders(new List<Tuple<string, string>>()
            {
                new("Authorization", "another secret")
            }), AzureFunctionTestFactory.CreateLogger());

        // Act

        // Assert
        var response = (StatusCodeResult) result;
        response.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
    }
}