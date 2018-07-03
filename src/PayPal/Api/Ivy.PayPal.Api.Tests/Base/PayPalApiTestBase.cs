using Ivy.IoC.Core;
using Ivy.PayPal.Api.IoC;
using Ivy.TestHelper;
using Ivy.Web.IoC;

namespace Ivy.PayPal.Api.Tests.Base
{
    public class PayPalApiTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyPayPalApi();
            containerGen.InstallIvyWeb();
        }
    }
}
