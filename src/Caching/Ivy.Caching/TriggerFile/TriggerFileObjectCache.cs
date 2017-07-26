using System;
using Microsoft.Extensions.Caching.Memory;
using Ivy.Caching.Core;
using Ivy.Caching.Core.TriggerFile;

namespace Ivy.Caching.TriggerFile
{
    public class TriggerFileObjectCache<T> :
        Basic.BasicObjectCache<T>, 
        IObjectCache<T>
    {
        #region Variables & Constants

        protected readonly ITriggerFileManager _TriggerFileManager;

        #endregion

        #region Constructor

        public TriggerFileObjectCache(
            IMemoryCache cache,
            ITriggerFileManager TriggerFileManager)
            :base(cache)
        {
            _TriggerFileManager = TriggerFileManager;
        }

        #endregion

        #region Public Methods

        public override T GetCache()
        {
            if (_getCacheFn != null)
            {
                RefreshIfNecessary();
            }

            return base.GetCache();
        }

        public override void Init(Func<T> getCache)
        {
            var cacheKey = GetCacheKey();

            if (_TriggerFileManager.ShouldRefreshCache<T>(cacheKey))
            {
                _TriggerFileManager.GenerateTriggerFile<T>(cacheKey);

                base.Init(getCache);
            }
        }

        #endregion

        #region Helper Methods

        protected void RefreshIfNecessary()
        {
            var cacheKey = GetCacheKey();

            if (_TriggerFileManager.ShouldRefreshCache<T>(cacheKey))
            {
                _cache.Remove(cacheKey);

                var itemsToCache = _getCacheFn();

                _cache.Set(cacheKey, itemsToCache);
            }
        }

        #endregion
    }
}
