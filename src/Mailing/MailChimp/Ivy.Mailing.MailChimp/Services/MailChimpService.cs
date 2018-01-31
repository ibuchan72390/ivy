using Ivy.Mailing.Core.Interfaces.Services;
using Ivy.Mailing.Core.Models;
using Ivy.Validation.Core.DomainModel;
using Ivy.Validation.Core.Interfaces;
using System.Threading.Tasks;
using Ivy.Mailing.Core.Enums;
using Ivy.Mailing.MailChimp.Core.Providers;

namespace Ivy.Mailing.MailChimp.Services
{
    public class MailChimpService : IMailingService
    {
        #region Variables & Constants

        private readonly IMailingApiHelper _apiHelper;

        private readonly IMailChimpConfigurationProvider _configProvider;

        #endregion

        #region Constructor

        public MailChimpService(
            IMailingApiHelper apiHelper,
            IMailChimpConfigurationProvider configProvider)
        {
            _apiHelper = apiHelper;
            _configProvider = configProvider;
        }

        #endregion

        #region Public Methods

        public async Task<IValidationResult> ProcessContactInfoAsync(MailingMember contactInfo)
        {
            // Validate new subscription status
            var member = await _apiHelper.GetMemberAsync(contactInfo.Email);

            if (member == null)
            {
               // Submit to mailchimp
               await _apiHelper.AddMemberAsync(contactInfo);

                return new ValidationResult(message: _configProvider.NewEnrollmentMessage);
            }
            else if (member.Status == MailingStatusName.Pending)
            {
                // Inform them of their pending status accordingly
                return new ValidationResult(true, _configProvider.PendingEnrollmentMessage);
            }
            else
            {
                // This is technically valid, I just don't want to sign them up again
                return new ValidationResult(true, _configProvider.AlreadyEnrolledMessage);
            }
        }

        #endregion
    }
}
