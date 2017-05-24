using Ivy.MailChimp.Core.Enums;
using Ivy.MailChimp.Core.Models;
using Ivy.MailChimp.Core.Services;
using Ivy.Validation;
using Ivy.Validation.Core;
using System.Threading.Tasks;

namespace Ivy.MailChimp.Services
{
    public class MailChimpService : IMailChimpService
    {
        #region Variables & Constants

        private readonly IMailChimpApiHelper _apiHelper;

        #endregion

        #region Constructor

        public MailChimpService(
            IMailChimpApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        #endregion

        #region Public Methods

        public async Task<IValidationResult> ProcessContactInfoAsync(MailChimpContactInfo contactInfo)
        {
            // Validate new subscription status
            var member = await _apiHelper.GetMemberAsync(contactInfo.email_address);

            if (member != null && member.status == MailChimpStatusName.subscribed.ToString())
            {
                // This is technically valid, I just don't want to sign them up again
                return new ValidationResult(true, "You have already signed up for our mailing list!");
            }

            if (member == null)
            {
               // Submit to mailchimp
               await _apiHelper.SaveContactInfoAsync(contactInfo);
            }
            else
            {
                // Update mailchimp accordingly
                bool alreadyPending = false;
                if (member.status == MailChimpStatusName.pending.ToString())
                    alreadyPending = true;

                if (alreadyPending)
                {
                    return new ValidationResult(true, "We have already received your email, " +
                        "but it does not appear that you have validated the acceptance email. " +
                        "Please check your inbox for a confirmation email.");
                }
                else
                {
                    member.status = MailChimpStatusName.pending.ToString();
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
