using Ivy.Caching.IoC;
using Ivy.IoC.Core;
using Ivy.TestHelper;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Caching.Test.Base
{
    public class BasicCachingTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyBasicCaching();

            var svcColl = containerGen.GetServiceCollection();

            svcColl.AddMemoryCache();
        }
    }
}
