using System.Net;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using TransPA.OpenSource;
using TransPA.OpenSource.Constants;
using TransPA.OpenSource.External.Datalon.Model;

namespace AzureFunctions.Test;

[TestFixture]
public class DataLonApiClientTest
{
    private DatalonApiClient _testee = null!;
    private Mock<IHttpClientFactory> _mockFactory = null!;
    private HttpClient _httpClient = null!;

    private const string TranspaEmployeeNumberAsString = "241010";
    private const string DatalonEmployeerId = "2020";
    private const string DatalonDuplicatedEmployeeNumber = "1010";
    private const string DatalonCorrectEmployeeId = "aaaa";
    private const string DatalonWrongEmployeeId = "bbbb";
    private const string DatalonCorrectSalaryPeriodCode = "24";
    private const string DatalonWrongSalaryPeriodCode = "34";


    [SetUp]
    public void SetUp()
    {
        var employees = GetListEmployeeEmployeesWhereTwoHaveTheSameEmployeeNumber();

        _mockFactory = new Mock<IHttpClientFactory>();
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock.Protected().Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        ).ReturnsAsync(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(employees))
        });
        //.Verifiable();
        _httpClient = new HttpClient(handlerMock.Object);

        _mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(_httpClient);
        _testee = new DatalonApiClient(_mockFactory.Object, new Mock<ILogger<DatalonApiClient>>().Object);
        
        Environment.SetEnvironmentVariable(DatalonApiConfigurationNameConstants.DatalonApiHost, "https://example.test");
    }

    private ResourceCollectionResponseBody<DatalonApiClient.EmployeeResponseBody> GetListEmployeeEmployeesWhereTwoHaveTheSameEmployeeNumber()
    {
        return new ResourceCollectionResponseBody<DatalonApiClient.EmployeeResponseBody>()
        {
            collection = new[]
            {
                new DatalonApiClient.EmployeeResponseBody()
                {
                    employeeNumber = DatalonDuplicatedEmployeeNumber,
                    salaryPeriodCode = DatalonCorrectSalaryPeriodCode,
                    employeeId = DatalonCorrectEmployeeId
                },
                new DatalonApiClient.EmployeeResponseBody()
                {
                    employeeNumber = DatalonDuplicatedEmployeeNumber,
                    salaryPeriodCode = DatalonWrongSalaryPeriodCode,
                    employeeId = DatalonWrongEmployeeId
                }
            }
        };
    }

    [Test]
    public async Task ReturnTheProperDatalonEmployeeIdWhenThereAreTwoEmployeesWithTheSameEmployeeNumber()
    {
        // Arrange

        // Act
        var employeeId = await _testee.GetEmployeeIdAsync(TranspaEmployeeNumberAsString, DatalonEmployeerId);

        // Assert
        employeeId.Should().Be(DatalonCorrectEmployeeId);
    }
}