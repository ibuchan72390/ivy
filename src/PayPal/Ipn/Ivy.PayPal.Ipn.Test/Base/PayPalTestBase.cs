using Ivy.PayPal.Ipn.IoC;
using Ivy.TestHelper;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.PayPal.Ipn.Test.Base
{
    public class PayPalTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                containerGen => containerGen.InstallIvyPayPalIpn(),
                svcColl => svcColl.AddLogging());
        }
    }
}
