using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Base.Blob;

namespace Ivy.Data.Common.Base.Blob
{
    public class BaseBlobService<TEntity, TRepo> : 
        IBlobService<TEntity, TRepo>

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

        public virtual string ConnectionString { get; private set; }

        public virtual void InitializeByConnectionString(string connectionString)
        {
            Repo.InitializeByConnectionString(connectionString);
        }

        public virtual void InitializeByDatabaseKey(string databaseKey)
        {
            Repo.InitializeByDatabaseKey(databaseKey);
        }

        #endregion
    }
}
