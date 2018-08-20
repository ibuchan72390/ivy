using Ivy.Data.Core.Interfaces.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ivy.Data.Core.Interfaces.Functional.Entity
{
    public interface ISaveOrUpdateEntity<TEntity, TKey>
        where TEntity: class, IEntityWithTypedId<TKey>
    {
        TEntity SaveOrUpdate(TEntity entity, ITranConn tc = null);
    }

    public interface ISaveOrUpdateEntityAsync<TEntity, TKey>
        where TEntity : class, IEntityWithTypedId<TKey>
    {
        Task<TEntity> SaveOrUpdateAsync(TEntity entity, ITranConn tc = null);
    }

    public interface ISaveOrUpdateEntities<TEntity, TKey>
        where TEntity : class, IEntityWithTypedId<TKey>
    {
        IEnumerable<TEntity> SaveOrUpdate(IEnumerable<TEntity> entities, ITranConn tc = null);
    }

    public interface ISaveOrUpdateEntitiesAsync<TEntity, TKey>
        where TEntity : class, IEntityWithTypedId<TKey>
    {
        Task<IEnumerable<TEntity>> SaveOrUpdateAsync(IEnumerable<TEntity> entities, ITranConn tc = null);
    }

    public interface IEntitySaveOrUpdate<TEntity, TKey> : 
        ISaveOrUpdateEntity<TEntity, TKey>,
        ISaveOrUpdateEntityAsync<TEntity, TKey>,
        ISaveOrUpdateEntities<TEntity, TKey>,
        ISaveOrUpdateEntitiesAsync<TEntity, TKey>
        where TEntity : class, IEntityWithTypedId<TKey>
    {
    }

    public interface IEntitySaveOrUpdate<TEntity> : IEntitySaveOrUpdate<TEntity, int>
        where TEntity : class, IEntity
    {
    }
}
