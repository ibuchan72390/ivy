using System;
using IBFramework.Core.Caching;
using IBFramework.Core.Data;

namespace IBFramework.Caching
{
    public class ObjectCache<T> : IObjectCache<T>
    {
        public T GetCache()
        {
            throw new NotImplementedException();
        }

        public T Init(Func<T> cacheLoadingFn, ITranConn tc = null)
        {
            throw new NotImplementedException();
        }


    }
}
