using Microsoft.Extensions.Caching.Memory;

namespace IBFramework.Caching.Core
{
    public interface ICacheAccessor
    {
        MemoryCache CacheInstance { get; }
    }
}
