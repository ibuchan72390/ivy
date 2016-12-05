using System.Collections.Generic;

namespace IB.Framework.Core.Data
{
    public interface IRepository<T, TKey>
        where T : IEntityWithTypedId<TKey>
    {
        T GetById(TKey id);

        IList<T> GetAll();

        T SaveOrUpdate(T entity, ITranConn tc = null);

        void Delete(T entity, ITranConn tc = null);

        void DeleteById(TKey id, ITranConn tc = null);
    }
}
