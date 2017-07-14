using Ivy.Auth0.Authorization.IoC;
using Ivy.TestHelper;
using Ivy.Web.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Auth0.Authorization.Test.Base
{
    public class Auth0AuthorizationTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                containerGen =>
                {
                    containerGen.InstallIvyWeb();
                    containerGen.InstallIvyAuth0Authorization();
                },
                svcColl =>
                {
                    svcColl.AddLogging();
                });
        }
    }
}
