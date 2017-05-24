using Ivy.Caching.Core;
using Ivy.Caching.IoC;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.TestHelper;
using Xunit;

namespace Ivy.Caching.Test
{
    public class CacheAccessorTests : TestBase
    {
        private IContainer _testContainer;

        public CacheAccessorTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            containerGen.InstallIvyCaching();

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
