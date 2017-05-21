using IBFramework.Data.Core.Domain;
using IBFramework.Data.Core.Interfaces.Domain;
using System.Collections.Generic;

namespace IBFramework.Data.Core.Domain
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
