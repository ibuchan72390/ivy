using System;
using IBFramework.Core.Data;

namespace IBFramework.Core.Caching
{
    public interface IObjectCache<T>
    {
        T Init(Func<T> cacheLoadingFn, ITranConn tc = null);

        T GetCache();
    }
}
