using Ivy.Auth0.IoC;
using Ivy.TestHelper;
using Ivy.Web.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Auth0.Test.Base
{
    public class Auth0TestBase : TestBase
    {
        protected override void InitWrapper()
        {
            //base.InitWrapper();
            base.Init(
                containerGen => 
                {
                    containerGen.InstallIvyWeb();
                    containerGen.InstallIvyAuth0();
                },
                svcColl => 
                {
                    svcColl.AddLogging();
                });
        }
    }
}
