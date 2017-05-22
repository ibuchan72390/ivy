using IBFramework.IoC.Core;
using IBFramework.MailChimp.Core.Services;
using IBFramework.MailChimp.Services;

namespace IBFramework.MailChimp.IoC
{
    public class MailChimpInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IMailChimpApiHelper, MailChimpApiHelper>();
            containerGenerator.RegisterSingleton<IMailChimpRequestFactory, MailChimpRequestFactory>();
            containerGenerator.RegisterSingleton<IMailChimpService, MailChimpService>();
        }
    }

    public static class MailChimpInstallerExtension
    {
        public static IContainerGenerator InstallMailChimp(this IContainerGenerator containerGenerator)
        {
            new MailChimpInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
