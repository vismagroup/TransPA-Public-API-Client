using transpa.api.generated.Model;
using TransPA.OpenSource.External.Datalon.Model;

namespace TransPA.OpenSource.External.Datalon;

public class SalaryConverter
{
    public Form Convert(Salary salary, string datalonEmployeeId)
    {
        return new Form()
        {
            date = salary.StartDate,
            entries = GetEntries(salary, datalonEmployeeId)
        };
    }

    private ICollection<Entry> GetEntries(Salary salary, string employeeId)
    {
        var entries = new List<Entry>();
        salary.WageRows.ForEach(w =>
        {
            var quantity = new Entry()
            {
                employeeId = employeeId,
                payTypeCode = $"00{w.PayTypeCode.Substring(0, 2)}",
                value = (int) (w.Quantity * 100)
            };

            var unitPrice = new Entry()
            {
                employeeId = employeeId,
                payTypeCode = $"00{w.PayTypeCode.Substring(2, 2)}",
                value = (int) (w.UnitPrice.Amount * 100)
            };

            entries.Add(quantity);
            entries.Add(unitPrice);
        });

        return entries;
    }
}