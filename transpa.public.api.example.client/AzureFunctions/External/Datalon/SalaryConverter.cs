using transpa.api.generated.Model;
using TransPA.OpenSource.External.Datalon.Model;

namespace TransPA.OpenSource.External.Datalon;

public class SalaryConverter
{
    public Form Convert(Salary salary, string datalonEmployeeId)
    {
        return new Form()
        {
            Date = salary.StartDate,
            Entries = GetEntries(salary, datalonEmployeeId)
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
            var quantity = GetWageRowQuantityEntry(employeeId, w);
            var unitPrice = GetWageRowUnitPriceEntry(employeeId, w);

            entries.Add(quantity);
            entries.Add(unitPrice);
        });
        
        salary.TimeRows.ForEach(t =>
        {
            if (String.IsNullOrEmpty(t.PayTypeCode))
            {
                return;
            }
            var quantity = GetTimeRowQuantityEntry(employeeId, t);

            entries.Add(quantity);
        });

        return entries;
    }

    private Entry GetWageRowQuantityEntry(string employeeId, SalaryWageRows salaryWageRows)
    {
        var entry = GetEntry(employeeId, salaryWageRows.Quantity);
        entry.PayTypeCode = $"00{salaryWageRows.PayTypeCode.Substring(0, 2)}";
        return entry;
    }
    
    private Entry GetWageRowUnitPriceEntry(string employeeId, SalaryWageRows salaryWageRows)
    {
        var entry = GetEntry(employeeId, salaryWageRows.UnitPrice.Amount);
        entry.PayTypeCode = $"00{salaryWageRows.PayTypeCode.Substring(2, 2)}";
        return entry;
    }
    
    private Entry GetTimeRowQuantityEntry(string employeeId, SalaryTimeRows salaryTimeRows)
    {
        var sum = salaryTimeRows.Details.Sum(x => x.Quantity);
        var entry = GetEntry(employeeId, sum);
        entry.PayTypeCode = $"00{salaryTimeRows.PayTypeCode.Substring(2, 2)}";
        return entry;
    }

    private Entry GetEntry(string employeeId, decimal value)
    {
        return new Entry()
        {
            EmployeeId = employeeId,
            Value = (int) (value * 100)
        };
    }
}