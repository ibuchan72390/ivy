using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Providers;
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

        private readonly IActiveCampaignConfigurationProvider _configProvider;

        #endregion

        #region Constructor
        
        public ActiveCampaignService(
            IMailingApiHelper apiHelper,
            IActiveCampaignConfigurationProvider configProvider)
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
                // Submit brand new contact
                await _apiHelper.AddMemberAsync(contactInfo);
            }
            else if (member != null && member.Status == MailingStatusName.Subscribed && IsInApplicationList(member))
            {
                // This is technically valid, I just don't want to sign them up again
                return new ValidationResult(true, _configProvider.AlreadyEnrolledMessage);
            }
            else if (!IsInApplicationList(member))
            {
                // Valid, but they need to be added to the next list
                contactInfo.ListIds.Add(_configProvider.ListId);

                await _apiHelper.EditMemberAsync(member);
            }

            // return response
            return new ValidationResult(message: _configProvider.NewEnrollmentMessage);
        }

        #endregion

        #region Helper Methods

        private bool IsInApplicationList(MailingMember member)
        {
            return member.ListIds.Contains(_configProvider.ListId);
        }

        #endregion
    }
}
