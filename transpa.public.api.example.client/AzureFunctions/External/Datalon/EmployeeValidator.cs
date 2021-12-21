using FluentValidation;
using Microsoft.Extensions.Logging;
using transpa.api.generated.Model;
using TransPA.OpenSource.External.Datalon.Model;

namespace TransPA.OpenSource.External.Datalon;

public class EmployeeValidator : AbstractValidator<Employee>
{
    private readonly ILogger<DatalonApiClient> _log;

    public EmployeeValidator(ILogger<DatalonApiClient> log)
    {
        _log = log;
        RuleFor(x => x.EmployeeNumber).Must(IsEmployeeNumberCorrectlyFormatted);
    }
    
    private bool IsEmployeeNumberCorrectlyFormatted(long? employeeNumber) {
        if (employeeNumber == null)
        {
            _log.LogWarning("EmployeeNumber is not set");
            return false;
        }

        var isEmployeeNumberCorrectlyFormatted = employeeNumber.Value.ToString().Length == 6;
        if (!isEmployeeNumberCorrectlyFormatted)
        {
            _log.LogWarning("EmployeeNumber is not of 6 in length");
        }
        
        return isEmployeeNumberCorrectlyFormatted;
    }
}