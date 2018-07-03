using Ivy.IoC.Core;
using Ivy.PayPal.Api.IoC;
using Ivy.PayPal.Api.Payments.IoC;
using Ivy.TestHelper;
using Ivy.Web.IoC;

namespace Ivy.PayPal.Api.Payments.Tests.Base
{
    public class PayPalPaymentsTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyWeb();
            containerGen.InstallIvyPayPalApi();
            containerGen.InstallIvyPayPalPaymentsApi();
        }
    }
}
