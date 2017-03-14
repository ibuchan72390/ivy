using IBFramework.Core.Data;
using IBFramework.Core.Data.Domain;
using IBFramework.Core.Data.Init;

namespace IBFramework.Data.Common.Base
{
    /*
     * You can probably repurpose this to use the generic key repository if necessary
     * It will be useful down the line when you finally want to use these entities.
     */

    public class BaseBlobService<TEntity, TRepo> : IInitialize
        where TEntity : class
        where TRepo : IBlobRepository<TEntity>
    {
        #region Attributes

        protected TRepo Repo;

        #endregion

        #region Constructor

        public BaseBlobService(TRepo repo)
        {
            Repo = repo;
        }

        #endregion

        #region Public Methods

        public string ConnectionString { get; private set; }

        public void InitializeByConnectionString(string connectionString)
        {
            Repo.InitializeByConnectionString(ConnectionString);
        }

        public void InitializeByDatabaseKey(string databaseKey)
        {
            Repo.InitializeByDatabaseKey(databaseKey);
        }

        #endregion
    }


    public class BaseEntityService<TEntity, TRepo> : IInitialize
        where TEntity : class, IEntity
        where TRepo : IRepository<TEntity>
    {
        #region Attributes

        protected TRepo Repo;

        #endregion

        #region Constructor

        public BaseEntityService(TRepo repo)
        {
            Repo = repo;
        }

        #endregion

        #region Public Methods

        public string ConnectionString { get; private set; }

        public void InitializeByConnectionString(string connectionString)
        {
            Repo.InitializeByConnectionString(ConnectionString);
        }

        public void InitializeByDatabaseKey(string databaseKey)
        {
            Repo.InitializeByDatabaseKey(databaseKey);
        }

        #endregion
    }
}
