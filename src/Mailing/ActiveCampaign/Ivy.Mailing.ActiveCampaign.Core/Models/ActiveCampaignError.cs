using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Models;

namespace Ivy.Mailing.ActiveCampaign.Core.Models
{
    public class ActiveCampaignError : IActiveCampaignModel
    {
        public int result_code { get; set; }
        public string result_message { get; set; }
        public string result_output { get; set; }
    }
}
