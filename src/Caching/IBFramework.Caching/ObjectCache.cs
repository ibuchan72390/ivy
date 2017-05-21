using System;
using Microsoft.Extensions.Caching.Memory;
using IBFramework.Caching.Core;

namespace IBFramework.Caching
{
    public class ObjectCache<T> : IObjectCache<T>
    {
        #region Variables & Constants

        protected readonly MemoryCache _cache;
        protected readonly ITriggerFileManager _TriggerFileManager;

        protected Func<T> _getCacheFn;

        #endregion

        #region Constructor

        public ObjectCache(ICacheAccessor cacheAccessor,
            ITriggerFileManager TriggerFileManager)
        {
            _cache = cacheAccessor.CacheInstance;
            _TriggerFileManager = TriggerFileManager;
        }

        #endregion

        #region Public Methods

        public T GetCache()
        {
            if (_getCacheFn != null)
            {
                RefreshIfNecessary();
            }
            return _cache.Get<T>(GetCacheKey());
        }

        public void Init(Func<T> getCache)
        {
            var cacheKey = GetCacheKey();

            if (_TriggerFileManager.ShouldRefreshCache<T>(cacheKey))
            {
                _TriggerFileManager.GenerateTriggerFile<T>(cacheKey);

                if (_getCacheFn == null)
                {
                    _getCacheFn = getCache;
                }

                var itemsToCache = _getCacheFn();

                _cache.Set(cacheKey, itemsToCache);
            }
        }

        public void RefreshCache()
        {
            _TriggerFileManager.TriggerCache<T>(GetCacheKey());
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

        /*
         * Override this method in order to create multiple custom object caches
         * that leverage the same type.  This will get passed to the TriggerFileManager
         * in order to provide an extra key to the type cache management.
         */
        protected virtual string GetCacheKey()
        {
            return "default";
        }

        protected void ExceptionIfNotInitialized()
        {
            if (_getCacheFn == null)
            {
                throw new Exception("Cache has not been initialized!");
            }
        }

        #endregion
    }
}
