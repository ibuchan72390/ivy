using Ivy.Mailing.Core.Interfaces.Transformers;
using Ivy.Mailing.MailChimp.Core.Models;

namespace Ivy.Mailing.MailChimp.Core.Interfaces.Transformers
{
    public interface IMailChimpExtraDataTransformer :
        IExtraDataMailingMemberTransformer<MailChimpMember>,
        IExtraDataContactModelTransformer<MailChimpContactInfo>
    {
    }
}
