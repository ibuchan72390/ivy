using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Base.Entity;
using Ivy.Data.Core.Interfaces.Domain;

namespace Ivy.Data.Common.Base.Entity
{
    /*
     * You can probably repurpose this to use the generic key repository if necessary
     * It will be useful down the line when you finally want to use these entities.
     */
    public class BaseEntityService<TEntity, TKey, TRepo> :
       IEntityService<TEntity, TKey, TRepo>

       where TEntity : class, IEntityWithTypedId<TKey>
       where TRepo : IEntityRepository<TEntity, TKey>
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
            ConnectionString = connectionString;
        }

        public virtual void InitializeByDatabaseKey(string databaseKey)
        {
            Repo.InitializeByDatabaseKey(databaseKey);
            ConnectionString = Repo.ConnectionString;
        }

        #endregion
    }


    public class BaseEntityService<TEntity, TRepo> : 
        BaseEntityService<TEntity, int, TRepo>,
        IEntityService<TEntity, TRepo>
        
        where TEntity : class, IEntity
        where TRepo : IEntityRepository<TEntity>
    {
        #region Constructor

        public BaseEntityService(TRepo repo)
            : base(repo)
        {
        }

        #endregion
    }
}
