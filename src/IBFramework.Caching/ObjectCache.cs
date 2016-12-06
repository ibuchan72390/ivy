using System;
using IBFramework.Core.Caching;
using IBFramework.Core.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace IBFramework.Caching
{
    public class ObjectCache<T> : IObjectCache<T>
    {
        #region Variables & Constants

        private readonly MemoryCache _cache;
        private readonly ITriggerFileGenerator _triggerFileGenerator;

        #endregion

        #region Constructor

        public ObjectCache(ICacheAccessor cacheAccessor,
            ITriggerFileGenerator triggerFileGenerator)
        {
            _cache = cacheAccessor.CacheInstance;
            _triggerFileGenerator = triggerFileGenerator;
        }

        #endregion

        #region Public Methods

        public T GetCache()
        {
            throw new NotImplementedException();
        }

        public T Init(Func<T> cacheLoadingFn, ITranConn tc = null)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
