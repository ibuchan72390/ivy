using Ivy.IoC.Core;
using Ivy.MailChimp.Core.Services;
using Ivy.MailChimp.Services;

namespace Ivy.MailChimp.IoC
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
        public static IContainerGenerator InstallIvyMailChimp(this IContainerGenerator containerGenerator)
        {
            new MailChimpInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
