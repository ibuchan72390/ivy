using IBFramework.Core.Data;
using IBFramework.Core.Domain;
using System;
using System.Collections.Generic;

namespace IBFramework.Data.MSSQL
{
    public class Repository<T> : IRepository<T>
    {
        public void DeleteAll(ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll()
        {
            throw new NotImplementedException();
        }
    }

    public class Repository<T, TKey> : Repository<T>, IRepository<T, TKey>
        where T : IEntityWithTypedId<TKey>
    {
        public void Delete(T entity, ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(TKey id, ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        public T GetById(TKey id)
        {
            throw new NotImplementedException();
        }

        public T SaveOrUpdate(T entity, ITranConn tc = null)
        {
            throw new NotImplementedException();
        }
    }
}
