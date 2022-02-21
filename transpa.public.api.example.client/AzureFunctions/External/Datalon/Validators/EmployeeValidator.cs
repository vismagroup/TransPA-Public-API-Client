using FluentValidation;
using Microsoft.Extensions.Logging;
using transpa.api.generated.Model;
using TransPA.OpenSource.External.Datalon.Model;

namespace TransPA.OpenSource.External.Datalon;

public class EmployeeValidator : AbstractValidator<Employee>
{
    internal const string EmployeeNumberNotSetMessage = "EmployeeNumber is not set";
    internal const string EmployeeNumberBadFormatMessage = "EmployeeNumber is not 6 characters long";
    internal const string EmployeeNumberUnknown = "failedEmployeeNumberUnknown";
    internal const string EmployeeNumberBadFormat = "failedEmployeeNumberBadFormat";

    public EmployeeValidator()
    {
        RuleFor(x => x.EmployeeNumber).Must(IsEmployeeNumberNotNull).WithErrorCode(EmployeeNumberUnknown).WithMessage(EmployeeNumberNotSetMessage);
        RuleFor(x => x.EmployeeNumber).Must(IsEmployeeNumberCorrectlyFormatted).WithErrorCode(EmployeeNumberBadFormat).WithMessage(EmployeeNumberBadFormatMessage);
    }

    private bool IsEmployeeNumberNotNull(long? employeeNumber) {
        return employeeNumber != null;
    }

    private bool IsEmployeeNumberCorrectlyFormatted(long? employeeNumber) {
       return employeeNumber != null && employeeNumber.Value.ToString().Length == 6;
    }
}