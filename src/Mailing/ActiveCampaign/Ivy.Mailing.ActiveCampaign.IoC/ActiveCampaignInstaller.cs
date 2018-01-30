using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Services;
using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Transformers;
using Ivy.Mailing.ActiveCampaign.Services;
using Ivy.Mailing.ActiveCampaign.Transformers;
using Ivy.IoC.Core;
using Ivy.Mailing.Core.Interfaces.Services;

namespace Ivy.Mailing.ActiveCampaign.IoC
{
    class ActiveCampaignInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            // Mailing Interfaces
            containerGenerator.RegisterSingleton<IMailingService, ActiveCampaignService>();
            containerGenerator.RegisterSingleton<IMailingApiHelper, ActiveCampaignApiHelper>();
            containerGenerator.RegisterSingleton<IMailingRequestFactory, ActiveCampaignRequestFactory>();

            // Active Campaign Interfaces
            containerGenerator.RegisterSingleton<IActiveCampaignContactListDeserializer, ActiveCampaignContactListDeserializer>();

            containerGenerator.RegisterSingleton<IActiveCampaignContactTransformer, ActiveCampaignContactTransformer>();
        }
    }

    public static class ActiveCampaignInstallerExtension
    {
        public static IContainerGenerator InstallIvyActiveCampaign(this IContainerGenerator containerGenerator)
        {
            new ActiveCampaignInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
