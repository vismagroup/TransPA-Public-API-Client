using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace TransPA.OpenSource.External.Datalon.Model;

public class Form
{
    public Form(string formId = default!, DateTime date = default!, string state = default!, string reference = default!, ICollection<Entry> entries = default!)
    {
        FormId = formId;
        Date = date;
        State = state;
        Reference = reference;
        Entries = entries;
    }

    [DataMember(Name = "formId", EmitDefaultValue = false)]
    public string FormId { get; set; }
    [DataMember(Name = "date", EmitDefaultValue = false)]
    public DateTime Date { get; set; }
    [DataMember(Name = "state", EmitDefaultValue = false)]
    public string State { get; set; }
    [DataMember(Name = "reference", EmitDefaultValue = false)]
    public string Reference { get; set; }
    [DataMember(Name = "entries", EmitDefaultValue = false)]
    public ICollection<Entry> Entries { get; set; }
}