using FluentAssertions;
using NUnit.Framework;
using transpa.api.generated.Model;
using TransPA.OpenSource.External.Datalon;

namespace AzureFunctions.Test;

[TestFixture]
public class SalaryConverterTest
{
    private SalaryConverter _testee = null!;

    private const string DatalonEmployeeId = "aaaa-bbbb";
    private const string PayTypeCode = "1213";
    private const string PayTypeCodeQuantityPart = "0012";
    private const string PayTypeCodeUnitPricePart = "0013";
    private const decimal Quantity = (decimal) 120.0;
    private const int QuantityInDatalonFormat = 12000;
    private const decimal UnitPrice = (decimal) 300.00;
    private const int UnitPriceInDatalonFormat = 30000;

    [SetUp]
    public void SetUp()
    {
        _testee = new SalaryConverter();
    }

    [Test]
    public void TwoEntriesAreCreatedFormOneWageRow()
    {
        // Arrange 
        var salary = new Salary()
        {
            WageRows = new List<SalaryWageRows>()
            {
                new SalaryWageRows()
                {
                    Quantity = Quantity,
                    UnitPrice = new Money(UnitPrice, "DKK"),
                    PayTypeCode = PayTypeCode
                }
            }
        };

        // Act
        var form = _testee.Convert(salary, DatalonEmployeeId);

        // Assert
        var formEntries = form.entries;
        formEntries.Count.Should().Be(2);
        var entries = formEntries.ToList();
        
        entries[0].employeeId.Should().Be(DatalonEmployeeId);
        entries[0].value.Should().Be(QuantityInDatalonFormat);
        entries[0].payTypeCode.Should().Be(PayTypeCodeQuantityPart);
        
        
        entries[1].employeeId.Should().Be(DatalonEmployeeId);
        entries[1].value.Should().Be(UnitPriceInDatalonFormat);
        entries[1].payTypeCode.Should().Be(PayTypeCodeUnitPricePart);
        
    }
}