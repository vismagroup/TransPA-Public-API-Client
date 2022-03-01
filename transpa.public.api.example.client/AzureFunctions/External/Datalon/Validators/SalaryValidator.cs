using FluentValidation;
using transpa.api.generated.Model;

namespace TransPA.OpenSource.External.Datalon;

public class SalaryValidator : AbstractValidator<Salary>
{
    internal const string WageRowBadFormat = "At least one wage row is in bad format";
    internal const string TimeRowBadFormat = "At least one time row is in bad format";
    internal const string NoRowsExported = "No payTypeCode is configured, meaning no rows will be exported";
    internal const string PayTypeCodeBadFormat = "failedPayTypeCodeBadFormat";
    internal const string PayTypeCodeUnknown = "failedPayTypeCodeUnknown";

    public SalaryValidator()
    {
        RuleFor(x => x.WageRows).Must(ValidatePayTypeCode).WithMessage(WageRowBadFormat).WithErrorCode(PayTypeCodeBadFormat);
        RuleFor(x => x.TimeRows).Must(ValidatePayTypeCode).WithMessage(TimeRowBadFormat).WithErrorCode(PayTypeCodeBadFormat);
        RuleFor(x => x).Must(ValidateThatThereIsAtLeastOneRowBeingExported).WithMessage(NoRowsExported).WithErrorCode(PayTypeCodeUnknown);
    }

    private bool ValidatePayTypeCode(List<SalaryWageRows> salaryWageRows)
    {
        return salaryWageRows.Any(x => x.PayTypeCode.Length is 0 or 4);
    }

    private bool ValidatePayTypeCode(List<SalaryTimeRows> salaryTimeRows)
    {
        return salaryTimeRows.Any(x => x.PayTypeCode.Length is 0 or 4);
    }

    private bool ValidateThatThereIsAtLeastOneRowBeingExported(Salary salary)
    {
        return salary.WageRows.Any(x => x.PayTypeCode != "") || salary.TimeRows.Any(x => x.PayTypeCode != "");
    }
}