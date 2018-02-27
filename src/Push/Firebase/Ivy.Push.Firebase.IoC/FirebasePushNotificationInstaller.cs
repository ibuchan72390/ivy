using Ivy.IoC.Core;
using Ivy.Push.Core.Interfaces.Services;
using Ivy.Push.Firebase.Services;

namespace Ivy.Push.Firebase.IoC
{
    public class FirebasePushNotificationInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IPushNotificationService, FirebasePushNotificationService>();
            containerGenerator.RegisterSingleton<IPushNotificationRequestFactory, FirebasePushNotificationRequestFactory>();        
        }
    }

    public static class FirebasePushNotificationInstallerExtension
    {
        public static IContainerGenerator InstallIvyFirebasePushNotifications(this IContainerGenerator containerGenerator)
        {
            new FirebasePushNotificationInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
