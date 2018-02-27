using Ivy.Google.Core.Interfaces.Services;
using Ivy.Google.Services;
using Ivy.IoC.Core;

namespace Ivy.Google.IoC
{
    class GoogleCoreInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator container)
        {
            container.RegisterSingleton<IGoogleAccessTokenGenerator, GoogleAccessTokenGenerator>();
        }
    }

    public static class GoogleCoreInstallerExtension
    {
        public static IContainerGenerator InstallIvyGoogleCore(this IContainerGenerator containerGenerator)
        {
            new GoogleCoreInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
