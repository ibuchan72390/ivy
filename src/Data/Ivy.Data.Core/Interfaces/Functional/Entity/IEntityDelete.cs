using Ivy.Data.Core.Interfaces.Domain;
using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces.Functional.Entity
{
    public interface IDeleteEntity<TEntity>
    {
        void Delete(TEntity entity, ITranConn tc = null);
    }

    public interface IDeleteEntityList<TEntity>
    {
        void Delete(IEnumerable<TEntity> entities, ITranConn tc = null);
    }

    public interface IDeleteEntityById<TKey>
    {
        void DeleteById(TKey id, ITranConn tc = null);
    }

    public interface IDeleteEntityByIdList<TKey>
    {
        void DeleteByIdList(IEnumerable<TKey> ids, ITranConn tc = null);
    }

    public interface IEntityDelete<TEntity, TKey> :
        IDeleteEntity<TEntity>,
        IDeleteEntityList<TEntity>,
        IDeleteEntityById<TKey>,
        IDeleteEntityByIdList<TKey>
        where TEntity : class, IEntityWithTypedId<TKey>
    {
    }

    public interface IEntityDelete<TEntity> : IEntityDelete<TEntity, int>
        where TEntity : class, IEntity
    {

    }

}
