using NUnit.Framework;
using TransPA.OpenSource.External.Datalon;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using FluentAssertions;
using transpa.api.generated.Model;

namespace AzureFunctions.Test.Validators
{
    public class SalaryValidatorTest
    {
        private SalaryValidator _salaryValidator = null!;

        private const string WrongPayTypeCodeForTimeRow = "500652";
        private const string WrongPayTypeCodeForWageRow = "600567";
        private const string StatusCodeUnknown = "failedPayTypeCodeUnknown";
        private const string StatusCodeBadFormat = "failedPayTypeCodeBadFormat";

        [SetUp]
        public void SetUp()
        {
            _salaryValidator = new SalaryValidator();
        }

        [TestCase("","")]
        public void ShouldReturnFalseIfStatusCodeIsNullOrEmpty(string payTypeCodeForTimeRow, string payTypeCodeForWageRow)
        {
            //Arrange
            var validInput = CreateSalary(payTypeCodeForTimeRow, payTypeCodeForWageRow);

            //Act
            var validatorResult = _salaryValidator.Validate(validInput);

            //Assert
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Select(x => x.ErrorCode).FirstOrDefault().Should().Be(StatusCodeUnknown);
        }

        [TestCase(WrongPayTypeCodeForTimeRow, WrongPayTypeCodeForWageRow)]
        public void ShouldReturnFalseIfPayTypeCodeBadFormat(string payTypeCodeForTimeRow, string payTypeCodeForWageRow)
        {
            //Arrange
            var validInput = CreateSalary(payTypeCodeForTimeRow, payTypeCodeForWageRow);

            //Act
            var validatorResult = _salaryValidator.Validate(validInput);

            //Assert
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Select(x => x.ErrorCode).FirstOrDefault().Should().Be(StatusCodeBadFormat);
        }

        private Salary CreateSalary(string payTypeCodeForTimeRow, string payTypeCodeForWageRow)
        {
            return new Salary()
            {
                EmployeeId = "241010",
                EndDate = DateTime.UtcNow,
                StartDate = DateTime.UtcNow,
                Id = "1002",
                TimeRows = CreateSalaryTimeRow(payTypeCodeForTimeRow),
                WageRows = CreateSalaryWageRow(payTypeCodeForWageRow),
            };
        }

        private List<SalaryWageRows> CreateSalaryWageRow(string payTypeCodeForWageRow)
        {
            return new List<SalaryWageRows>
            {
                new SalaryWageRows() {
                PayTypeCode = payTypeCodeForWageRow,
                Quantity = 5,
                UnitPrice = new Money(100, "SEK"),
                Details = new List<SalaryDetails>(),
                }
            };
        }

        private List<SalaryTimeRows> CreateSalaryTimeRow(string payTypeCodeForWageRow)
        {
            return new List<SalaryTimeRows>
            {
                new SalaryTimeRows() {
                    PayTypeCode = payTypeCodeForWageRow,
                    Details = new List<SalaryDetails1>(),
                }
            };
        }

    }
}
