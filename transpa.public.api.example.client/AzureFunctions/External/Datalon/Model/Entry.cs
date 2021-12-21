using System.Diagnostics.CodeAnalysis;

namespace TransPA.OpenSource.External.Datalon.Model;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
public class Entry
{
    public string entryId { get; set; } = null!;
    public string employeeId { get; set; } = null!;
    public string payTypeCode { get; set; } = null!;
    public int value { get; set; }
    public string comment { get; set; } = null!;
}