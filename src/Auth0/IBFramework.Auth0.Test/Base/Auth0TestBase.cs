using IBFramework.Auth0.IoC;
using IBFramework.TestHelper;

namespace IBFramework.Auth0.Test.Base
{
    public class Auth0TestBase : TestBase
    {
        protected override void InitWrapper()
        {
            //base.InitWrapper();
            base.Init(containerGen => 
            {
                containerGen.InstallAuth0();
            });
        }
    }
}
