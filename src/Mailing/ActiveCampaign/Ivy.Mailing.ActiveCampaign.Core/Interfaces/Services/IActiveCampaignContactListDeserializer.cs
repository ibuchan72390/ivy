using Ivy.Mailing.ActiveCampaign.Core.Models;

namespace Ivy.Mailing.ActiveCampaign.Core.Interfaces.Services
{
    public interface IActiveCampaignContactListDeserializer
    {
        ActiveCampaignContactList Deserialize(string json);
    }
}
