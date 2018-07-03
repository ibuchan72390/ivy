using Ivy.IoC.Core;
using Ivy.PayPal.Ipn.IoC;
using Ivy.TestHelper;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.PayPal.Ipn.Test.Base
{
    public class PayPalTestBase<T> : TestBase<T>
        where T: class
    {
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            containerGen.InstallIvyPayPalIpn();

            var svcColl = containerGen.GetServiceCollection();

            svcColl.AddLogging();
        }
    }
}
