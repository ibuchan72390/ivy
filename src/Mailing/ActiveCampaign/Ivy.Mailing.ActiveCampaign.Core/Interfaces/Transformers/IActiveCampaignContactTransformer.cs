using Ivy.Mailing.ActiveCampaign.Core.Models;
using Ivy.Mailing.Core.Models;

namespace Ivy.Mailing.ActiveCampaign.Core.Interfaces.Transformers
{
    public interface IActiveCampaignContactTransformer
    {
        MailingMember Transform(ActiveCampaignContact contact);
    }
}
