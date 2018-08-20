using Ivy.Data.Core.Interfaces.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ivy.Data.Core.Interfaces.Functional.Entity
{
    public interface IDeleteEntity<TEntity>
    {
        void Delete(TEntity entity, ITranConn tc = null);
    }

    public interface IDeleteEntityAsync<TEntity>
    {
        Task DeleteAsync(TEntity entity, ITranConn tc = null);
    }

    public interface IDeleteEntityList<TEntity>
    {
        void Delete(IEnumerable<TEntity> entities, ITranConn tc = null);
    }

    public interface IDeleteEntityListAsync<TEntity>
    {
        Task DeleteAsync(IEnumerable<TEntity> entities, ITranConn tc = null);
    }

    public interface IDeleteEntityById<TKey>
    {
        void DeleteById(TKey id, ITranConn tc = null);
    }

    public interface IDeleteEntityByIdAsync<TKey>
    {
        Task DeleteByIdAsync(TKey id, ITranConn tc = null);
    }

    public interface IDeleteEntityByIdList<TKey>
    {
        void DeleteByIdList(IEnumerable<TKey> ids, ITranConn tc = null);
    }

    public interface IDeleteEntityByIdListAsync<TKey>
    {
        Task DeleteByIdListAsync(IEnumerable<TKey> ids, ITranConn tc = null);
    }

    public interface IEntityDelete<TEntity, TKey> :
        IDeleteEntity<TEntity>,
        IDeleteEntityAsync<TEntity>,
        IDeleteEntityList<TEntity>,
        IDeleteEntityListAsync<TEntity>,
        IDeleteEntityById<TKey>,
        IDeleteEntityByIdAsync<TKey>,
        IDeleteEntityByIdList<TKey>,
        IDeleteEntityByIdListAsync<TKey>
        where TEntity : class, IEntityWithTypedId<TKey>
    {
    }

    public interface IEntityDelete<TEntity> : IEntityDelete<TEntity, int>
        where TEntity : class, IEntity
    {

    }

}
