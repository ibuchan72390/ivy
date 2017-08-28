using Ivy.Data.Core.Interfaces.Domain;
using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces.Functional.Entity
{
    public interface IEntityDelete<TEntity, TKey>
        where TEntity : class, IEntityWithTypedId<TKey>
    {
        void Delete(TEntity entity, ITranConn tc = null);

        void Delete(IEnumerable<TEntity> entities, ITranConn tc = null);

        void DeleteById(TKey id, ITranConn tc = null);

        void DeleteByIdList(IEnumerable<TKey> ids, ITranConn tc = null);
    }

    public interface IEntityDelete<TEntity> : IEntityDelete<TEntity, int>
        where TEntity : class, IEntity
    {

    }

}
