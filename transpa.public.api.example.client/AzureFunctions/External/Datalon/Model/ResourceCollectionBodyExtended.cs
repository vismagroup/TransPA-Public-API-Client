using System.Runtime.Serialization;

namespace TransPA.OpenSource.External.Datalon.Model;

public class ResourceCollectionBodyExtended<T> : ResourceCollectionBody<T>
{
    [DataMember(Name = "page", EmitDefaultValue = false)]

    public long Page { get; set; }
    [DataMember(Name = "pageSize", EmitDefaultValue = false)]
    public long PageSize { get; set; }
    [DataMember(Name = "availablePages", EmitDefaultValue = false)]
    public long AvailablePages { get; set; }
    [DataMember(Name = "totalCount", EmitDefaultValue = false)]
    public long TotalCount { get; set; }
}