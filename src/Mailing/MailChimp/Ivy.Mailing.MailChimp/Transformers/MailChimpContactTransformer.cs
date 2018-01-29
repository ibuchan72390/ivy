using Ivy.Mailing.MailChimp.Core.Interfaces.Transformers;
using Ivy.Mailing.MailChimp.Core.Models;
using Ivy.Mailing.Core.Models;
using Ivy.Mailing.Core.Interfaces.Transformers;
using System;
using Ivy.Mailing.Core.Enums;

namespace Ivy.Mailing.MailChimp.Transformers
{
    public class MailChimpContactTransformer : IMailChimpContactTransformer
    {
        #region Variables & Constants

        private readonly IExtraDataMailingMemberTransformer<MailChimpMember> _mailingMemberTransformer;

        private readonly IExtraDataContactModelTransformer<MailChimpContactInfo> _contactModelTransformer;

        #endregion

        #region Constructor

        public MailChimpContactTransformer(
            IExtraDataMailingMemberTransformer<MailChimpMember> extraDataTransformer,
            IExtraDataContactModelTransformer<MailChimpContactInfo> contactModelTransformer)
        {
            _mailingMemberTransformer = extraDataTransformer;
            _contactModelTransformer = contactModelTransformer;
        }

        #endregion

        #region Public Methods

        public MailingMember Transform(MailChimpMember member)
        {
            var mailingMember = new MailingMember
            {
                Id = member.id,
                Email = member.email_address
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

            return _mailingMemberTransformer.Transform(mailingMember, member);
        }

        public MailChimpContactInfo Transform(MailingMember member)
        {
            var mailChimpMember = new MailChimpContactInfo
            {
                email_address = member.Email,
            };

            mailChimpMember.SetStatus(member.Status);

            return _contactModelTransformer.Transform(mailChimpMember, member);
        }

        #endregion
    }
}
