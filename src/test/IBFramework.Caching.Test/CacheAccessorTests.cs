using IBFramework.Caching.IoC;
using IBFramework.Core.Caching;
using IBFramework.Core.IoC;
using IBFramework.IoC;
using IBFramework.TestHelper;
using Xunit;

namespace IBFramework.Caching.Test
{
    public class CacheAccessorTests : TestBase
    {
        private IContainer _testContainer;

        public CacheAccessorTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            containerGen.InstallCaching();

            _testContainer = containerGen.GenerateContainer();
        }

        [Fact]
        public void CacheAccessor_Returns_Singleton_Instance_Of_Cache()
        {
            var cacheAccessor1 = _testContainer.Resolve<ICacheAccessor>();
            var cacheAccessor2 = _testContainer.Resolve<ICacheAccessor>();

            Assert.Same(cacheAccessor1.CacheInstance, cacheAccessor2.CacheInstance);
        }
    }
}
