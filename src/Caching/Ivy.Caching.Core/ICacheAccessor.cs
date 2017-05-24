using Microsoft.Extensions.Caching.Memory;

namespace Ivy.Caching.Core
{
    public interface ICacheAccessor
    {
        MemoryCache CacheInstance { get; }
    }
}
