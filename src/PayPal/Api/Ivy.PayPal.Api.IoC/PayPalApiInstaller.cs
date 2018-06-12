using Ivy.IoC.Core;
using Ivy.PayPal.Api.Core.Interfaces.Services;
using Ivy.PayPal.Api.Services;

namespace Ivy.PayPal.Api.IoC
{
    public class PayPalApiInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IPayPalApiTokenGenerator, PayPalApiTokenGenerator>();
            containerGenerator.RegisterSingleton<IPayPalApiTokenRequestGenerator, PayPalApiTokenRequestGenerator>();
            containerGenerator.RegisterSingleton<IPayPalUrlGenerator, PayPalUrlGenerator>();
        }
    }

    public static class PayPalApiInstallerExtension
    {
        public static IContainerGenerator InstallIvyPayPalApi(this IContainerGenerator containerGenerator)
        {
            new PayPalApiInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
