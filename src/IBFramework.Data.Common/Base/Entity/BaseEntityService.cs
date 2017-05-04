using IBFramework.Core.Data;
using IBFramework.Core.Data.Base.Entity;
using IBFramework.Core.Data.Domain;

namespace IBFramework.Data.Common.Base.Entity
{
    /*
     * You can probably repurpose this to use the generic key repository if necessary
     * It will be useful down the line when you finally want to use these entities.
     */

    public class BaseEntityService<TEntity, TRepo> : 
        IEntityService<TEntity, TRepo>
        
        where TEntity : class, IEntity
        where TRepo : IEntityRepository<TEntity>
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
