using Ivy.IoC.Core;
using Ivy.PayPal.Api.Payments.Core.Interfaces.Services;
using Ivy.PayPal.Api.Payments.Services;

namespace Ivy.PayPal.Api.Payments.IoC
{
    public class PayPalPaymentsApiInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IPayPalPaymentsRequestGenerator, PayPalPaymentsRequestGenerator>();
            containerGenerator.RegisterSingleton<IPayPalPaymentsService, PayPalPaymentsService>();
        }
    }

    public static class PayPalPaymentsApiInstallerExtension
    {
        public static IContainerGenerator InstallIvyPayPalPaymentsApi(this IContainerGenerator containerGenerator)
        {
            new PayPalPaymentsApiInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
