using Ivy.Auth0.Web.IoC;
using Ivy.TestHelper;
using Ivy.Web.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Auth0.Web.Test.Base
{
    public class Auth0WebTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                containerGen =>
                {
                    containerGen.InstallIvyWeb();
                    containerGen.InstallIvyAuth0Web();
                },
                svcColl =>
                {
                    svcColl.AddLogging();
                });
        }
    }
}
