using Ivy.PayPal.Api.IoC;
using Ivy.PayPal.Api.Payments.IoC;
using Ivy.TestHelper;
using Ivy.Web.IoC;

namespace Ivy.PayPal.Api.Payments.Tests.Base
{
    public class PayPalPaymentsTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(containerGen => 
            {
                containerGen.InstallIvyWeb();
                containerGen.InstallIvyPayPalApi();
                containerGen.InstallIvyPayPalPaymentsApi();
            });
        }
    }
}
