using Ivy.Auth0.Management.IoC;
using Ivy.TestHelper;
using Ivy.Web.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Auth0.Management.Test.Base
{
    public class Auth0ManagementTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                containerGen => 
                {
                    containerGen.InstallIvyWeb();
                    containerGen.InstallIvyAuth0Management();
                },
                svcColl => 
                {
                    svcColl.AddLogging();
                });
        }
    }
}
