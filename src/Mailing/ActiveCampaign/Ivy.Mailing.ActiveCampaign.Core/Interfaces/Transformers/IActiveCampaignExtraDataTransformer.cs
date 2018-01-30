using Ivy.Mailing.ActiveCampaign.Core.Models;
using Ivy.Mailing.Core.Interfaces.Transformers;

namespace Ivy.Mailing.ActiveCampaign.Core.Interfaces.Transformers
{
    public interface IActiveCampaignExtraDataTransformer :
        IExtraDataMailingMemberTransformer<ActiveCampaignContact>
    {
    }
}
