using IBFramework.Core.Data.Domain;
using System.Collections.Generic;

namespace IBFramework.Core.Data.Functional.Entity
{
    public interface IEntityGet<TEntity, TKey>
        where TEntity : class, IEntityWithTypedId<TKey>
    {
        TEntity GetById(TKey id, ITranConn tc = null);

        IEnumerable<TEntity> GetByIdList(IEnumerable<TKey> ids, ITranConn tc = null);
    }

    public interface IEntityGet<TEntity> : IEntityGet<TEntity, int>
        where TEntity : class, IEntity
    {
    }
}
