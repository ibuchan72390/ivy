using IBFramework.Core.Data.Domain;

namespace IBFramework.Core.Domain
{
    public class EntityWithTypedId<T> : IEntityWithTypedId<T>
    {
        public T Id { get; set; }
    }
}
