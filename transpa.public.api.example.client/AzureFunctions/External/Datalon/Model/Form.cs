namespace TransPA.OpenSource.External.Datalon.Model;

public class Form
{
    public string formId { get; set; } = null!;
    public DateTime date { get; set; }
    public string state { get; set; } = null!;
    // public string reference { get; set; } = null!;
    public ICollection<Entries> entries { get; set; } = null!;
}