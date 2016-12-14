using IBFramework.Core.Data.Domain;

namespace IBFramework.Domain
{
    public class EntityWithTypedId<TKey> : IEntityWithTypedId<TKey>
    {
        public TKey Id { get; set; }
    }
}
