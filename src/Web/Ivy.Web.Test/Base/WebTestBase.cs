using Ivy.IoC.Core;
using Ivy.TestHelper;
using Ivy.Web.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Web.Test.Base
{
    public class WebTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyWeb();

            var svcColl = containerGen.GetServiceCollection();

            svcColl.AddLogging();
        }
    }
}
