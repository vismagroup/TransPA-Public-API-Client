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
            if (String.IsNullOrEmpty(w.PayTypeCode))
            {
                return;
            }
            var quantity = GetQuantityEntry(employeeId, w);
            var unitPrice = GetUnitPriceEntry(employeeId, w);

            entries.Add(quantity);
            entries.Add(unitPrice);
        });
        
        salary.TimeRows.ForEach(t =>
        {
            if (String.IsNullOrEmpty(t.PayTypeCode))
            {
                return;
            }
            var quantity = GetTimeRowQuantity(employeeId, t);

            entries.Add(quantity);
        });

        return entries;
    }

    private Entry GetQuantityEntry(string employeeId, SalaryWageRows salaryWageRows)
    {
        var entry = GetEntry(employeeId, salaryWageRows.Quantity);
        entry.payTypeCode = $"00{salaryWageRows.PayTypeCode.Substring(0, 2)}";
        return entry;
    }
    
    private Entry GetUnitPriceEntry(string employeeId, SalaryWageRows salaryWageRows)
    {
        var entry = GetEntry(employeeId, salaryWageRows.UnitPrice.Amount);
        entry.payTypeCode = $"00{salaryWageRows.PayTypeCode.Substring(2, 2)}";
        return entry;
    }
    
    private Entry GetTimeRowQuantity(string employeeId, SalaryTimeRows salaryTimeRows)
    {
        var sum = salaryTimeRows.Details.Sum(x => x.Quantity);
        var entry = GetEntry(employeeId, sum);
        entry.payTypeCode = $"00{salaryTimeRows.PayTypeCode.Substring(2, 2)}";
        return entry;
    }

    private Entry GetEntry(string employeeId, decimal value)
    {
        return new Entry()
        {
            employeeId = employeeId,
            value = (int) (value * 100)
        };
    }
}