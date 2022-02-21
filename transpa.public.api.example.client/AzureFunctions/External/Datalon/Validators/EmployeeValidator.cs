using FluentValidation;
using Microsoft.Extensions.Logging;
using transpa.api.generated.Model;
using TransPA.OpenSource.External.Datalon.Model;

namespace TransPA.OpenSource.External.Datalon;

public class EmployeeValidator : AbstractValidator<Employee>
{
    private readonly ILogger<EmployeeValidator> _log;

    internal const string EmployeeNumberUnknown = "failedEmployeeNumberUnknown";
    internal const string EmployeeNumberBadFormat = "failedEmployeeNumberBadFormat";

    public EmployeeValidator(ILogger<EmployeeValidator> log)
    {
        _log = log;
        RuleFor(x => x.EmployeeNumber).Must(IsEmployeeNumberNotNull).WithErrorCode(EmployeeNumberUnknown);
        RuleFor(x => x.EmployeeNumber).Must(IsEmployeeNumberCorrectlyFormatted).WithErrorCode(EmployeeNumberBadFormat);
    }

    private bool IsEmployeeNumberNotNull(long? employeeNumber) {
        var isEmployeeNumberNotNull= true;
        if (employeeNumber == null)
        {
            _log.LogWarning("EmployeeNumber is not set");
            return false;
        }

        return isEmployeeNumberNotNull;
    }

    private bool IsEmployeeNumberCorrectlyFormatted(long? employeeNumber) {
        var isEmployeeNumberCorrectlyFormatted = true;
        if (employeeNumber != null)
        {
            isEmployeeNumberCorrectlyFormatted = employeeNumber.Value.ToString().Length == 6;
            if (!isEmployeeNumberCorrectlyFormatted)
            {
                _log.LogWarning("EmployeeNumber is not 6 characters long");
            }
        }
        
        return isEmployeeNumberCorrectlyFormatted;
    }
}