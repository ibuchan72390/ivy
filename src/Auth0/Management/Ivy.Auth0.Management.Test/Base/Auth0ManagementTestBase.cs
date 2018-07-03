using Ivy.Auth0.Management.IoC;
using Ivy.IoC.Core;
using Ivy.TestHelper;
using Ivy.Web.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Auth0.Management.Test.Base
{
    public class Auth0ManagementTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyWeb();
            containerGen.InstallIvyAuth0Management();

            var svcColl = containerGen.GetServiceCollection();

            svcColl.AddLogging();
        }
    }
}
