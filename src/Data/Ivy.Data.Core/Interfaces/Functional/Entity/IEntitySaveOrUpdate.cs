using Ivy.Data.Core.Interfaces.Domain;
using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces.Functional.Entity
{
    public interface ISaveOrUpdateEntity<TEntity, TKey>
        where TEntity: class, IEntityWithTypedId<TKey>
    {
        TEntity SaveOrUpdate(TEntity entity, ITranConn tc = null);
    }

    public interface ISaveOrUpdateEntities<TEntity, TKey>
        where TEntity : class, IEntityWithTypedId<TKey>
    {
        IEnumerable<TEntity> SaveOrUpdate(IEnumerable<TEntity> entities, ITranConn tc = null);
    }

    public interface IEntitySaveOrUpdate<TEntity, TKey> : 
        ISaveOrUpdateEntity<TEntity, TKey>,
        ISaveOrUpdateEntities<TEntity, TKey>
        where TEntity : class, IEntityWithTypedId<TKey>
    {
    }

    public interface IEntitySaveOrUpdate<TEntity> : IEntitySaveOrUpdate<TEntity, int>
        where TEntity : class, IEntity
    {
    }
}
