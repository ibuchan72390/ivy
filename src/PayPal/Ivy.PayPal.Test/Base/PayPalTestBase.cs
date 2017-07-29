using Ivy.PayPal.IoC;
using Ivy.TestHelper;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.PayPal.Test.Base
{
    public class PayPalTestBase : TestBase
    {
        protected override void InitWrapper()
        {
            base.Init(
                containerGen => {

                    containerGen.InstallIvyPayPal();
                },
                svcColl => {

                    svcColl.AddLogging();
                });
        }
    }
}
