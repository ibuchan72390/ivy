using Ivy.IoC.Core;
using Ivy.PayPal.Ipn.Core.Interfaces.Services;
using Ivy.PayPal.Ipn.Core.Interfaces.Transformer;
using Ivy.PayPal.Ipn.Services;
using Ivy.PayPal.Ipn.Transformers;

namespace Ivy.PayPal.Ipn.IoC
{
    public class PayPalIpnInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            // Services
            containerGenerator.RegisterSingleton<IPayPalIpnService, PayPalIpnService>();
            containerGenerator.RegisterSingleton<IPayPalIpnValidator, PayPalIpnValidator>();
            containerGenerator.RegisterSingleton<IPayPalRequestGenerator, PayPalRequestGenerator>();

            // Transformers
            containerGenerator.RegisterSingleton<IPayPalIpnResponseTransformer, PayPalIpnResponseTransformer>();
        }
    }

    public static class PayPalIpnInstallerExtension
    {
        public static IContainerGenerator InstallIvyPayPalIpn(this IContainerGenerator containerGenerator)
        {
            new PayPalIpnInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
