using Ivy.Caching.IoC;
using Ivy.IoC.Core;
using Ivy.TestHelper;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Caching.Test.Base
{
    public class TriggerFileCachingTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyTriggerFileCaching();

            var svcColl = containerGen.GetServiceCollection();

            svcColl.AddMemoryCache();
        }
    }
}
