using System.Diagnostics.CodeAnalysis;

namespace TransPA.OpenSource.External.Datalon.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    [SuppressMessage("ReSharper", "UnusedType.Local")]
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
    public class ResourceCollectionResponseBody<T>
    {
        public IEnumerable<T> collection { get; set; } = null!;
    }
}