using Ivy.Mailing.MailChimp.Core.Models;
using Ivy.Mailing.Core.Models;

namespace Ivy.Mailing.MailChimp.Core.Interfaces.Transformers
{
    public interface IMailChimpContactTransformer
    {
        MailingMember Transform(MailChimpMember member);
    }
}
