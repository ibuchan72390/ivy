using Ivy.Mailing.Core.Interfaces.Services;
using Ivy.Mailing.Core.Models;
using Ivy.Validation.Core.DomainModel;
using Ivy.Validation.Core.Interfaces;
using System.Threading.Tasks;
using Ivy.Mailing.Core.Enums;

namespace Ivy.Mailing.MailChimp.Services
{
    public class MailChimpService : IMailingService
    {
        #region Variables & Constants

        private readonly IMailingApiHelper _apiHelper;

        #endregion

        #region Constructor

        public MailChimpService(
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
                // Update mailchimp accordingly
                if (member.Status == MailingStatusName.Pending)
                {
                    return new ValidationResult(true, "We have already received your email, " +
                        "but it does not appear that you have validated the acceptance email. " +
                        "Please check your inbox for a confirmation email.");
                }
                else
                {
                    member.Status = MailingStatusName.Pending;
                    await _apiHelper.EditMemberAsync(member);
                }
            }

            // return response
            return new ValidationResult(message: "We have successfully received your contact information.  " +
                "You should receive an email in your inbox shortly requesting you to confirm our mailing list. " +
                "Please confirm the email or you may not receive our newsletter.");
        }

        #endregion
    }
}
