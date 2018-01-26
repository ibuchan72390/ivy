using Ivy.IoC.Core;
using Ivy.Mailing.MailChimp.Services;
using Ivy.Mailing.Core.Interfaces.Services;
using Ivy.Mailing.MailChimp.Transformers;
using Ivy.Mailing.MailChimp.Core.Interfaces.Transformers;

namespace Ivy.Mailing.MailChimp.IoC
{
    public class MailChimpInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IMailingApiHelper, MailChimpApiHelper>();
            containerGenerator.RegisterSingleton<IMailingRequestFactory, MailChimpRequestFactory>();

            containerGenerator.RegisterSingleton<IMailingService, MailChimpService>();

            containerGenerator.RegisterSingleton<IMailChimpContactTransformer, MailChimpContactTransformer>();
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
