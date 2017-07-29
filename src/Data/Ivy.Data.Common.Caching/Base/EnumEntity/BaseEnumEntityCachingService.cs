using Ivy.Caching.Core;
using Ivy.Data.Common.Base.EnumEntity;
using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Domain;
using System;
using System.Collections.Generic;

namespace Ivy.Data.Common.Caching.Base.EnumEntity
{
    public class BaseEnumEntityCachingService<TEntity, TRepo, TEnum> : 
        BaseEnumEntityService<TEntity, TRepo, TEnum>

        where TEntity : class, IEnumEntity<TEnum>
        where TRepo : IEnumEntityRepository<TEntity, TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        #region Variables & Constants

        protected readonly IObjectCache<IEnumerable<TEntity>> _cache;

        #endregion

        #region Constructor

        public BaseEnumEntityCachingService(
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
