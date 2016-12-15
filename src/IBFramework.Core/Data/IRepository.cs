using IBFramework.Core.Data.Domain;
using System.Collections.Generic;

namespace IBFramework.Core.Data
{
    public interface IRepository<TObject>
    {
        IList<TObject> GetAll();

        void DeleteAll(ITranConn tc = null);
    }

    public interface IRepository<TEntity, TKey> : IRepository<TEntity>
        where TEntity : IEntityWithTypedId<TKey>
    {
        TEntity GetById(TKey id);

        TEntity SaveOrUpdate(TEntity entity, ITranConn tc = null);

        void Delete(TEntity entity, ITranConn tc = null);

        void DeleteById(TKey id, ITranConn tc = null);
    }
}
