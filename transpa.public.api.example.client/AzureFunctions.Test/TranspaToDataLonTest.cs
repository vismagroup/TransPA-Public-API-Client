using AzureFunctions.Test.TestUtils;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using transpa.api.generated.Model;
using TransPA.OpenSource;
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

    private const string TranspaEmployeeId = "transpa-guid";

    [SetUp]
    public void SetUp()
    {
        _salaryCreated = new SalaryCreated(tenantId: "aaaa-bbbb-cccc-dddd", title: "A salary resource was created",
            resourceUrl: "https://example.test");

        _publicApiClientMock = new Mock<IPublicApiClient>();
        _datalonApiClientMock = new Mock<IDatalonApiClient>();
        _testee = new TranspaToDatalon(_publicApiClientMock.Object, _datalonApiClientMock.Object, new SalaryConverter(),
            new EmployeeValidator(new Mock<ILogger<DatalonApiClient>>().Object), new HttpObjectResultHelper(), new SalaryValidator());
    }

    [Test]
    public async Task EmployeeNumberMissingShouldReturnBadRequest()
    {
        // Arrange
        var employeeNumberMissing = new Employee(employeeNumber: null);
        _publicApiClientMock.Setup(x => x.GetEmployeeAsync(It.IsAny<string>())).ReturnsAsync(employeeNumberMissing);
        _publicApiClientMock.Setup(x => x.GetSalaryAsync(_salaryCreated)).ReturnsAsync(new Salary(employeeId: TranspaEmployeeId));

        // Act
        var result = await _testee.ExportSalaryFromTranspaToDatalon(JsonConvert.SerializeObject(_salaryCreated),
            AzureFunctionTestFactory.CreateHttpRequest(new List<Tuple<string, string>>()), AzureFunctionTestFactory.CreateLogger());

        // Assert
        var response = (ObjectResult) result;
        response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Test]
    public async Task EmployeeNumberInABadFormatForDatalonShouldReturnBadRequest()
    {
        // Arrange
        var employeeWithEmployeeNumberWithBadFormat = new Employee(employeeNumber: 24035);
        _publicApiClientMock.Setup(x => x.GetEmployeeAsync(It.IsAny<string>())).ReturnsAsync(employeeWithEmployeeNumberWithBadFormat);
        _publicApiClientMock.Setup(x => x.GetSalaryAsync(_salaryCreated)).ReturnsAsync(new Salary(employeeId: TranspaEmployeeId));

        // Act
        var result = await _testee.ExportSalaryFromTranspaToDatalon(JsonConvert.SerializeObject(_salaryCreated),
            AzureFunctionTestFactory.CreateHttpRequest(new List<Tuple<string, string>>()), AzureFunctionTestFactory.CreateLogger());

        // Assert
        var response = (ObjectResult) result;
        response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [TestCase(SalaryValidator.NoRowsExported, "", "")]
    [TestCase(SalaryValidator.WageRowBadFormat, "12345", "")]
    [TestCase(SalaryValidator.TimeRowBadFormat, "", "12345")]
    public async Task NoPayCodeSetShouldReturnBadRequest(String expectedResult, string wageRowPayTypeCode, string timeRowPayTypeCode)
    {
        // Arrange
        var employee = new Employee(employeeNumber: 240035);
        _publicApiClientMock.Setup(x => x.GetEmployeeAsync(It.IsAny<string>())).ReturnsAsync(employee);
        var salary = new Salary(employeeId: TranspaEmployeeId, wageRows: new List<SalaryWageRows>()
        {
            new SalaryWageRows(wageRowPayTypeCode, new Money(100, "SEK"), 200)
        }, timeRows: new List<SalaryTimeRows>()
        {
            new SalaryTimeRows(timeRowPayTypeCode)
        });
        _publicApiClientMock.Setup(x => x.GetSalaryAsync(_salaryCreated)).ReturnsAsync(salary);

        // Act
        var result = await _testee.ExportSalaryFromTranspaToDatalon(JsonConvert.SerializeObject(_salaryCreated),
            AzureFunctionTestFactory.CreateHttpRequest(new List<Tuple<string, string>>()), AzureFunctionTestFactory.CreateLogger());

        // Assert
        var response = (ObjectResult) result;
        response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        response.Value.GetType().Should().Be(typeof(ProblemDetails));
        var details = (ProblemDetails)response.Value;
        details.Detail.Should().Be(expectedResult);
    }
}