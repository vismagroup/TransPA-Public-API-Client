namespace TransPA.OpenSource.External.Datalon.Model;

public class ResourceCollectionResponseBodyExtended<T> : ResourceCollectionResponseBody<T>
{
    public long page { get; set; }
    public long pageSize { get; set; }
    public long availablePages { get; set; }
    public long totalCount { get; set; }
}