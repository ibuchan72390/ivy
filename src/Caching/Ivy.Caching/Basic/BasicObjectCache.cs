using Ivy.Caching.Core;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Ivy.Caching.Basic
{
    public class BasicObjectCache<T> : 
        IObjectCache<T>
    {
        #region Variables & Constants

        protected readonly Type _cacheType;
        protected readonly IMemoryCache _cache;

        protected Func<T> _getCacheFn;

        #endregion

        #region Constructor

        public BasicObjectCache(
            IMemoryCache cache)
        {
            _cache = cache;

            _cacheType = typeof(T);
        }

        #endregion

        #region Public Methods

        public virtual T GetCache()
        {
            T result;

            if (!_cache.TryGetValue(GetCacheKey(), out result))
            {
                _cache.Set(GetCacheKey(), _getCacheFn());
            }

            return result;
        }

        public virtual void Init(Func<T> getCache)
        {
            var cacheKey = GetCacheKey();

            // After initialization, don't let it overwrite
            if (_getCacheFn == null)
            {
                _getCacheFn = getCache;
            }

            _cache.Set(cacheKey, _getCacheFn());
        }

        public virtual void RefreshCache()
        {
            if (_getCacheFn == null)
            {
                throw new Exception("Unable to refresh cache!  It has not yet been initialized!");
            }

            _cache.Remove(GetCacheKey());

            _cache.Set(GetCacheKey(), _getCacheFn());
        }

        #endregion

        #region Helper Methods

        protected virtual string GetCacheKeyIdentifier()
        {
            return "default";
        }

        /*
         * Override this method in order to create multiple custom object caches
         * that leverage the same type.  This will get passed to the TriggerFileManager
         * in order to provide an extra key to the type cache management.
         */
        protected string GetCacheKey()
        {
            // Perhaps we should cache this key value somehow...

            var key = $"{GetCacheKeyIdentifier()}-{_cacheType.AssemblyQualifiedName}";

            key = key.Replace("`", "-");
            key = key.Replace("[", "");
            key = key.Replace("]", "");

            return key;
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
