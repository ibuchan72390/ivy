using Ivy.Mailing.Core.Enums;
using Ivy.Mailing.Core.Interfaces.Services;
using Ivy.Mailing.Core.Models;
using Ivy.Validation.Core.DomainModel;
using Ivy.Validation.Core.Interfaces;
using System.Threading.Tasks;

namespace Ivy.Mailing.ActiveCampaign.Services
{
    public class ActiveCampaignService : IMailingService
    {
        #region Variables & Constants

        private readonly IMailingApiHelper _apiHelper;

        #endregion

        #region Constructor
        
        public ActiveCampaignService(
            IMailingApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
        
        #endregion

        #region Public Methods

        public async Task<IValidationResult> ProcessContactInfoAsync(MailingMember contactInfo)
        {
            // Validate new subscription status
            var member = await _apiHelper.GetMemberAsync(contactInfo.Email);

            if (member != null && member.Status == MailingStatusName.Subscribed)
            {
                // This is technically valid, I just don't want to sign them up again
                return new ValidationResult(true, "You have already signed up for our mailing list!");
            }

            if (member == null)
            {
                // Submit to mailchimp
                await _apiHelper.AddMemberAsync(contactInfo);
            }
            else
            {
                await _apiHelper.EditMemberAsync(member);
            }

            // return response
            return new ValidationResult(message: "We have successfully received your contact " + 
                "information and you have been added to our mailing list.");
        }

        #endregion
    }
}
