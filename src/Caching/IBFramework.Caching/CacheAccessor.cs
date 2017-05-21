using IBFramework.Caching.Core;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace IBFramework.Caching
{
    public class CacheAccessor : ICacheAccessor
    {
        #region Variables & Constants

        private MemoryCache _cacheInstance;

        #endregion

        #region Constructor

        public CacheAccessor()
        {
            var memCacheOptions = new MemoryCacheOptions();
            var cacheOptions = Options.Create(memCacheOptions);
            _cacheInstance = new MemoryCache(cacheOptions);
        }

        #endregion

        #region Public Methods

        public MemoryCache CacheInstance => _cacheInstance;

        #endregion
    }
}
