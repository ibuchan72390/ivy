using Ivy.PayPal.Api.IoC;
using Ivy.TestHelper;
using Ivy.Web.IoC;

namespace Ivy.PayPal.Api.Tests.Base
{
    public class PayPalApiTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(containerGen => 
            {
                containerGen.InstallIvyPayPalApi();
                containerGen.InstallIvyWeb();
            });
        }
    }
}
