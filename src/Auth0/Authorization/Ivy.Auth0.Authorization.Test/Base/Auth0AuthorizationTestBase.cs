using Ivy.Auth0.Authorization.IoC;
using Ivy.IoC.Core;
using Ivy.TestHelper;
using Ivy.Web.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Auth0.Authorization.Test.Base
{
    public class Auth0AuthorizationTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyWeb();
            containerGen.InstallIvyAuth0Authorization();

            var svcColl = containerGen.GetServiceCollection();

            svcColl.AddLogging();
        }
    }
}
