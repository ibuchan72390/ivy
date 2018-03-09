using Ivy.IoC.Core;
using Ivy.Push.Web.Core.Interfaces.Services;
using Ivy.Push.Web.Services;

namespace Ivy.Push.Web.IoC
{
    public class WebPushNotificationInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IWebPushClientGenerator, WebPushClientGenerator>();
            containerGenerator.RegisterSingleton<IWebPushClientService, WebPushClientService>();
            containerGenerator.RegisterSingleton<IWebPushNotificationService, WebPushNotificationService>();
        }
    }

    public static class WebPushNotificationInstallerExtension
    {
        public static IContainerGenerator InstallIvyWebPushNotifications(this IContainerGenerator containerGenerator)
        {
            new WebPushNotificationInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
