using System;
using IB.Framework.Core.Data;

namespace IB.Framework.Core.Caching
{
    public interface IObjectCache<T>
    {
        T Init(Func<T> cacheLoadingFn, ITranConn tc = null);

        T GetCache();
    }
}
