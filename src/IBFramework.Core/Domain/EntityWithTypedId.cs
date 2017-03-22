using System.Collections.Generic;
using IBFramework.Core.Data.Domain;

namespace IBFramework.Core.Domain
{
    public class EntityWithTypedId<T> : BaseEntityWithReferences, IEntityWithTypedId<T>
    {
        public EntityWithTypedId()
        {
            References = new Dictionary<string, object>();
        }

        public T Id { get; set; }
    }
}
