namespace TransPA.OpenSource.External.Datalon.Model;

public class Entry
{
    public string entryId { get; set; } = null!;
    public string employeeId { get; set; } = null!;
    public string payTypeCode { get; set; } = null!;
    public int value { get; set; }
    public string comment { get; set; } = null!;
}