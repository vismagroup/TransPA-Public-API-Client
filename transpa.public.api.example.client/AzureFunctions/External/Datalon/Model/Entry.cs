using System.Runtime.Serialization;

namespace TransPA.OpenSource.External.Datalon.Model;

public class Entry
{
    public Entry(string entryId = default!, string employeeId = default!, string payTypeCode = default!, int value = default, string comment = default!)
    {
        EntryId = entryId;
        EmployeeId = employeeId;
        PayTypeCode = payTypeCode;
        Value = value;
        Comment = comment;
    }

    [DataMember(Name = "entryId", EmitDefaultValue = false)]
    public string EntryId { get; set; }
    [DataMember(Name = "employeeId", EmitDefaultValue = false)]
    public string EmployeeId { get; set; }

    [DataMember(Name = "payTypeCode", EmitDefaultValue = false)]
    public string PayTypeCode { get; set; }

    [DataMember(Name = "value", EmitDefaultValue = false)]
    public int Value { get; set; }
    [DataMember(Name = "comment", EmitDefaultValue = false)]
    public string Comment { get; set; }
}