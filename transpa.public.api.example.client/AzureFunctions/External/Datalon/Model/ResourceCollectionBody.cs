using System.Diagnostics.CodeAnalysis;

namespace TransPA.OpenSource.External.Datalon.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
    public class ResourceCollectionBody<T>
    {
        public ICollection<T> collection { get; set; } = null!;
    }
}
