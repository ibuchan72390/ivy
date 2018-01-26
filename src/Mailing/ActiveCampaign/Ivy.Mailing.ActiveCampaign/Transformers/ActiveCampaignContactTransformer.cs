using Ivy.Mailing.ActiveCampaign.Core.Interfaces.Transformers;
using Ivy.Mailing.ActiveCampaign.Core.Models;
using Ivy.Mailing.Core.Enums;
using Ivy.Mailing.Core.Interfaces.Transformers;
using Ivy.Mailing.Core.Models;
using System;

namespace Ivy.Mailing.ActiveCampaign.Transformers
{
    public class ActiveCampaignContactTransformer : IActiveCampaignContactTransformer
    {
        #region Variables & Constants

        private readonly IExtraDataTransformer<ActiveCampaignContact> _extraDataTransformer;

        #endregion

        #region Constructor

        public ActiveCampaignContactTransformer(
            IExtraDataTransformer<ActiveCampaignContact> extraDataTransformer)
        {
            _extraDataTransformer = extraDataTransformer;
        }

        #endregion

        #region Public Methods

        public MailingMember Transform(ActiveCampaignContact contact)
        {
            var member = new MailingMember
            {
                Id = contact.id,
                Email = contact.email,
                FirstName = contact.first_name,
                LastName = contact.last_name,
                Phone = contact.phone
            };

            switch (contact.status)
            {
                case ("1"):
                    member.Status = MailingStatusName.Subscribed;
                    break;
                case ("2"):
                    member.Status = MailingStatusName.Unsubscribed;
                    break;
                default:
                    throw new Exception($"Unknown ActiveCampaignContact.status received! status: {contact.status}");
            }

            return _extraDataTransformer.Transform(member, contact);
        }

        #endregion
    }
}
