namespace Ivy.Mailing.ActiveCampaign.Core.Interfaces.Models
{
    public interface IActiveCampaignModel
    {
        int result_code { get; set; }
        string result_message { get; set; }
        string result_output { get; set; }
    }
}
