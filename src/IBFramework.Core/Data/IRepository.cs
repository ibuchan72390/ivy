using IBFramework.Core.Data.Domain;
using IBFramework.Core.Data.Init;
using System.Collections.Generic;

namespace IBFramework.Core.Data
{
    public interface IBaseRepository<TObject> : IInitialize
    {
        IEnumerable<TObject> GetAll();

        void DeleteAll(ITranConn tc = null);
    }

    public interface IRepository<TEntity> : IBaseRepository<TEntity>
    {
        void Insert(TEntity entity, ITranConn tc = null);

        void BulkInsert(IEnumerable<TEntity> entities, ITranConn tc = null);
    }

    public interface IRepository<TEntity, TKey> : IBaseRepository<TEntity>
        where TEntity : IEntityWithTypedId<TKey>
    {
        TEntity GetById(TKey id);

        IEnumerable<TEntity> GetByIdList(IEnumerable<TKey> ids);

        TEntity SaveOrUpdate(TEntity entity, ITranConn tc = null);

        IEnumerable<TEntity> BulkSaveOrUpdate(IEnumerable<TEntity> entities, ITranConn tc = null);

        void Delete(TEntity entity, ITranConn tc = null);

        void DeleteById(TKey id, ITranConn tc = null);
    }
}
