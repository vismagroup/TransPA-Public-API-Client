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
    private const decimal Quantity = (decimal) 120.01;
    private const int QuantityInDatalonFormat = 12001;
    private const decimal UnitPrice = (decimal) 300.02;
    private const int UnitPriceInDatalonFormat = 30002;
    
    private const string PayTypeCodeTimeRow = "0001";
    private const decimal TimeRowQuantity = (decimal) 8.02;
    private const int TimeRowQuantityInDatalonFormat = 802;

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
            },
            TimeRows = new List<SalaryTimeRows>()
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

    [Test]
    public void TimeWageRowIsRepresentedProperly()
    {
        // Arrange 
        var salary = new Salary()
        {
            WageRows = new List<SalaryWageRows>(),
            TimeRows = new List<SalaryTimeRows>()
            {
                new SalaryTimeRows()
                {
                    PayTypeCode = PayTypeCodeTimeRow,
                    Details = new List<SalaryDetails1>()
                    {
                        new SalaryDetails1()
                        {
                            Quantity = TimeRowQuantity
                        }
                    }
                }
            }
        };

        // Act
        var form = _testee.Convert(salary, DatalonEmployeeId);

        // Assert
        var formEntries = form.entries;
        formEntries.Count.Should().Be(1);
        var entries = formEntries.ToList();

        var entry = entries[0];
        entry.employeeId.Should().Be(DatalonEmployeeId);
        entry.value.Should().Be(TimeRowQuantityInDatalonFormat);
        entry.payTypeCode.Should().Be(PayTypeCodeTimeRow);
    }
    
    [Test]
    public void EntriesThatAreMissingPayCodeAreNotAdded()
    {
        // Arrange 
        var salary = new Salary()
        {
            WageRows = new List<SalaryWageRows>()
            {
                new SalaryWageRows()
                {
                    PayTypeCode = ""
                }
            },
            TimeRows = new List<SalaryTimeRows>()
            {
                new SalaryTimeRows()
                {
                    PayTypeCode = ""
                }
            }
        };

        // Act
        var form = _testee.Convert(salary, DatalonEmployeeId);

        // Assert
        var formEntries = form.entries;
        formEntries.Count.Should().Be(0);
    }
}