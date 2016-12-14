using System;

namespace IBFramework.Core.Caching
{
    public interface IObjectCache<T>
    {
        void Init(Func<T> getCache);

        T GetCache();

        void RefreshCache();
    }
}
