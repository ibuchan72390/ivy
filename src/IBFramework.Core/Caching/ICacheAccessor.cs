using Microsoft.Extensions.Caching.Memory;

namespace IBFramework.Core.Caching
{
    public interface ICacheAccessor
    {
        MemoryCache CacheInstance { get; }
    }
}
