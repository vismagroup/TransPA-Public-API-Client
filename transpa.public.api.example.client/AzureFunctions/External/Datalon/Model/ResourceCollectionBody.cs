using System.Runtime.Serialization;

namespace TransPA.OpenSource.External.Datalon.Model
{
    public class ResourceCollectionBody<T>
    {
        [DataMember(Name = "collection", EmitDefaultValue = false)]
        public ICollection<T> Collection { get; set; } = null!;
    }
}
