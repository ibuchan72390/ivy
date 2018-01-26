using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Models;
using System.Collections.Generic;

namespace Ivy.Mailing.ActiveCampaign.Core.Models
{
    public class ActiveCampaignContactList : Dictionary<int, ActiveCampaignContact>, IActiveCampaignModel
    {
        public int result_code { get; set; }
        public string result_message { get; set; }
        public string result_output { get; set; }
    }
}
