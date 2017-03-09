using System;
using System.Collections.Generic;
using IBFramework.Core.Data;
using IBFramework.Core.Data.Domain;

namespace IBFramework.Data.Common
{
    public class Repository<T> : IRepository<T>
    {
        #region Variables & Constants

        protected readonly IDatabaseKeyManager _databaseKeyManager;

        public string ConnectionString { get; private set; }

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
            ConnectionString = connectionString;

            throw new NotImplementedException();
        }

        public void InitializeByDatabaseKey(string databaseKey)
        {
            InitializeByConnectionString(_databaseKeyManager.GetConnectionString(databaseKey));
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

        #region Helper Methods



        #endregion
    }

    public class Repository<T, TKey> : Repository<T>, IRepository<T, TKey>
        where T : IEntityWithTypedId<TKey>
    {
        #region Constructor

        public Repository(IDatabaseKeyManager databaseKeyManager) 
            : base(databaseKeyManager)
        {
        }

        #endregion

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
