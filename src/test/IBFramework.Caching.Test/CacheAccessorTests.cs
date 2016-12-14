using IBFramework.Core.Caching;
using IBFramework.TestHelper;
using Xunit;

namespace IBFramework.Caching.Test
{
    public class CacheAccessorTests : TestBase
    {
        [Fact]
        public void CacheAccessor_Returns_Singleton_Instance_Of_Cache()
        {
            var cacheAccessor1 = TestServiceLocator.StaticContainer.Resolve<ICacheAccessor>();
            var cacheAccessor2 = TestServiceLocator.StaticContainer.Resolve<ICacheAccessor>();

            Assert.Same(cacheAccessor1.CacheInstance, cacheAccessor2.CacheInstance);
        }
    }
}
