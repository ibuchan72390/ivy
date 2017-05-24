using Ivy.Data.Core.Domain;
using Ivy.Data.Core.Interfaces.Domain;
using System.Collections.Generic;

namespace Ivy.Data.Core.Domain
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
