using System;

namespace IBFramework.Caching.Core
{
    public interface IObjectCache<T>
    {
        void Init(Func<T> getCache);

        T GetCache();

        void RefreshCache();
    }
}
