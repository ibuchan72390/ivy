using IBFramework.Core.Data.Domain;
using IBFramework.Core.Data.Init;
using System.Collections.Generic;

namespace IBFramework.Core.Data
{
    public interface IBaseRepository<TObject> : IInitialize
        where TObject : class
    {
        string ConnectionString { get; }

        IEnumerable<TObject> GetAll(ITranConn tc = null);

        void DeleteAll(ITranConn tc = null);
    }

    public interface IBlobRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        void Insert(TEntity entity, ITranConn tc = null);

        void BulkInsert(IEnumerable<TEntity> entities, ITranConn tc = null);
    }

    public interface IEntityRepository<TEntity, TKey> : IBaseRepository<TEntity>
        where TEntity : class, IEntityWithTypedId<TKey>
    {
        TEntity GetById(TKey id, ITranConn tc = null);

        IEnumerable<TEntity> GetByIdList(IEnumerable<TKey> ids, ITranConn tc = null);

        TEntity SaveOrUpdate(TEntity entity, ITranConn tc = null);

        //IEnumerable<TEntity> BulkSaveOrUpdate(IEnumerable<TEntity> entities, ITranConn tc = null);

        void Delete(TEntity entity, ITranConn tc = null);

        void DeleteById(TKey id, ITranConn tc = null);
    }

    public interface IEntityRepository<TEntity> : IEntityRepository<TEntity, int>
        where TEntity : class, IEntity
    {
    }
}
