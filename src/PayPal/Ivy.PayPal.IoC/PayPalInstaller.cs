using Ivy.IoC.Core;
using Ivy.PayPal.Core.Interfaces.Services;
using Ivy.PayPal.Core.Interfaces.Transformer;
using Ivy.PayPal.Services;
using Ivy.PayPal.Transformers;

namespace Ivy.PayPal.IoC
{
    public class PayPalInstaller : IContainerInstaller
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

    public static class PayPalInstallerExtension
    {
        public static IContainerGenerator InstallIvyPayPal(this IContainerGenerator containerGenerator)
        {
            new PayPalInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
