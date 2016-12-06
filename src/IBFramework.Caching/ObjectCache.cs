using System;
using IB.Framework.Core.Caching;
using IB.Framework.Core.Data;

namespace IB.Framework.Caching
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
