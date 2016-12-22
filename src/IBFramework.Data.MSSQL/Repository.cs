using IBFramework.Core.Data;
using IBFramework.Core.Data.Domain;
using System;
using System.Collections.Generic;

namespace IBFramework.Data.MSSQL
{
    public class Repository<T> : IRepository<T>
    {
        #region Variables & Constants

        protected IDatabaseKeyManager _databaseKeyManager;

        

        #endregion

        #region Constructor

        public Repository(IDatabaseKeyManager databaseKeyManager)
        {
            _databaseKeyManager = databaseKeyManager;
        }

        #endregion

        #region Initialization

        public void InitializeByConnectionString(string connectionString)
        {
            throw new NotImplementedException();
        }

        public void InitializeByDatabaseKey(string databaseKey)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public Methods

        public void DeleteAll(ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll()
        {
            throw new NotImplementedException();
        }

        #endregion
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
