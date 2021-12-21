using System.Diagnostics.CodeAnalysis;

namespace TransPA.OpenSource.External.Datalon.Model;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class Form
{
    public string formId { get; set; } = null!;
    public DateTime date { get; set; }
    public string state { get; set; } = null!;
    public string reference { get; set; } = null!;
    public ICollection<Entry> entries { get; set; } = null!;
}