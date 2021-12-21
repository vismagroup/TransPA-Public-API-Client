using System.Diagnostics.CodeAnalysis;

namespace TransPA.OpenSource.External.Datalon.Model;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class ResourceCollectionBodyExtended<T> : ResourceCollectionBody<T>
{
    public long page { get; set; }
    public long pageSize { get; set; }
    public long availablePages { get; set; }
    public long totalCount { get; set; }
}