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

        [SetUp]
        public void SetUp()
        {
            _salaryValidator = new SalaryValidator();
        }

        [TestCase("")]
        public void ShouldReturnFalseIfStatusCodeIsNullOrEmpty(string payTypeCode)
        {
            //Arrange
            var validInput = CreateSalary(payTypeCode);

            //Act
            var validatorResult = _salaryValidator.Validate(validInput);

            //Assert
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Select(x => x.ErrorCode).FirstOrDefault().Should().Be("failedPayTypeCodeUnknown");
        }

        [TestCase("000001")]
        public void ShouldReturnFalseIfPayTypeCodeBadFormat(string payTypeCode)
        {
            //Arrange
            var validInput = CreateSalary(payTypeCode);

            //Act
            var validatorResult = _salaryValidator.Validate(validInput);

            //Assert
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Select(x => x.ErrorCode).FirstOrDefault().Should().Be("failedPayTypeCodeBadFormat");

        }

        private Salary CreateSalary(string payTypeCode)
        {
            var salary = new Salary();

            return new Salary()
            {
                EmployeeId = "100001",
                EndDate = DateTime.UtcNow,
                StartDate = DateTime.UtcNow,
                Id = "1002",
                TimeRows = CreateSalaryTimeRows(payTypeCode),
                WageRows = CreateSalaryWageRows(payTypeCode),
            };
        }

        private List<SalaryWageRows> CreateSalaryWageRows(string payTypeCode)
        {
            return new List<SalaryWageRows>
            {
                new SalaryWageRows() {
                PayTypeCode = payTypeCode,
                Quantity = 5,
                UnitPrice = new Money(100, "SEK"),
                Details = new List<SalaryDetails>(),
                }
            };
        }

        private List<SalaryTimeRows> CreateSalaryTimeRows(string payTypeCode)
        {
            return new List<SalaryTimeRows>
            {
                new SalaryTimeRows() {
                    PayTypeCode = payTypeCode,
                    Details = new List<SalaryDetails1>(),
                }
            };
        }

    }
}
