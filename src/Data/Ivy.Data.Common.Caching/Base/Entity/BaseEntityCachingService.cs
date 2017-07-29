using Ivy.Caching.Core;
using Ivy.Data.Common.Base.Entity;
using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Domain;
using System.Collections.Generic;

namespace Ivy.Data.Common.Caching.Base.Entity
{
    public class BaseEntityCachingService<TEntity, TRepo> :
        BaseEntityService<TEntity, TRepo>

        where TEntity : class, IEntity
        where TRepo : IEntityRepository<TEntity>
    {
        #region Variables & Constants

        protected readonly IObjectCache<IEnumerable<TEntity>> _cache;

        #endregion

        #region Constructor

        public BaseEntityCachingService(
            TRepo repo,
            IObjectCache<IEnumerable<TEntity>> cache) 
            : base(repo)
        {
            _cache = cache;

            _cache.Init(InitializeCache);
        }

        protected virtual IEnumerable<TEntity> InitializeCache()
        {
            return Repo.GetAll();
        }

        #endregion
    }
}
