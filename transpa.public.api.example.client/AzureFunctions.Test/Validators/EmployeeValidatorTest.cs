using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using transpa.api.generated.Model;
using TransPA.OpenSource;
using TransPA.OpenSource.External.Datalon;

namespace AzureFunctions.Test.Validators
{
    public class EmployeeValidatorTest
    {
        private EmployeeValidator _employeeValidator = null!;

        [SetUp]
        public void SetUp()
        {
            _employeeValidator = new EmployeeValidator(new Mock<ILogger<EmployeeValidator>>().Object);
        }

        [TestCase(null)]
        public void ShouldReturnFalseIfStatusCodeIsNullOrEmpty(long? employeeNumber)
        {
            //Arrange
            var validInput = CreateEmployee(employeeNumber);

            //Act
            var validatorResult = _employeeValidator.Validate(validInput);

            //Assert
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Select(x => x.ErrorCode).FirstOrDefault().Should().Be("failedEmployeeNumberUnknown");
        }

        [TestCase("2410100")]
        public void ShouldReturnFalseIfPayTypeCodeBadFormat(long? employeeNumber)
        {
            //Arrange
            var validInput = CreateEmployee(employeeNumber);

            //Act
            var validatorResult = _employeeValidator.Validate(validInput);

            //Assert
            validatorResult.IsValid.Should().BeFalse();
            validatorResult.Errors.Select(x => x.ErrorCode).FirstOrDefault().Should().Be("failedEmployeeNumberBadFormat");

        }

        private Employee CreateEmployee(long? employeeNumber)
        {
            return new Employee()
            {
                Id = "000111",
                EmployeeNumber = employeeNumber,
                FirstName = "Test",
                LastName = "Test",
                IsActive = true,
                Signature = "TST"
            };
        }
    }
}
