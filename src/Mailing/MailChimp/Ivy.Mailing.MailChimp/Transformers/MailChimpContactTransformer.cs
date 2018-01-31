using Ivy.Mailing.MailChimp.Core.Interfaces.Transformers;
using Ivy.Mailing.MailChimp.Core.Models;
using Ivy.Mailing.Core.Models;
using System;
using Ivy.Mailing.Core.Enums;
using Ivy.Mailing.MailChimp.Core.Providers;
using System.Collections.Generic;

namespace Ivy.Mailing.MailChimp.Transformers
{
    public class MailChimpContactTransformer : IMailChimpContactTransformer
    {
        #region Variables & Constants

        private readonly IMailChimpExtraDataTransformer _extraDataTransformer;

        private readonly IMailChimpConfigurationProvider _configProvider;

        #endregion

        #region Constructor

        public MailChimpContactTransformer(
            IMailChimpExtraDataTransformer extraDataTransformer)
        {
            _extraDataTransformer = extraDataTransformer;
        }

        #endregion

        #region Public Methods

        public MailingMember Transform(MailChimpMember member)
        {
            var mailingMember = new MailingMember
            {
                Id = member.id,
                Email = member.email_address,
                ListIds = new List<string>
                {
                    _configProvider.ListId
                }
            };

            switch (member.status)
            {
                case ("subscribed"):
                    mailingMember.Status = MailingStatusName.Subscribed;
                    break;
                case ("unsubscribed"):
                    mailingMember.Status = MailingStatusName.Unsubscribed;
                    break;
                case ("pending"):
                    mailingMember.Status = MailingStatusName.Pending;
                    break;
                default:
                    throw new Exception($"Unknown MailChimp member status received! Status: {member.status}");
            }

            return _extraDataTransformer.Transform(mailingMember, member);
        }

        public MailChimpContactInfo Transform(MailingMember member)
        {
            var mailChimpMember = new MailChimpContactInfo
            {
                email_address = member.Email,
            };

            mailChimpMember.SetStatus(member.Status);

            return _extraDataTransformer.Transform(mailChimpMember, member);
        }

        #endregion
    }
}
