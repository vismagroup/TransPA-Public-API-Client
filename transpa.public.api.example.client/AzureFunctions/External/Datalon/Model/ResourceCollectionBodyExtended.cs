namespace TransPA.OpenSource.External.Datalon.Model;

public class ResourceCollectionBodyExtended<T> : ResourceCollectionBody<T>
{
    public long page { get; set; }
    public long pageSize { get; set; }
    public long availablePages { get; set; }
    public long totalCount { get; set; }
}