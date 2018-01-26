using Ivy.Mailing.Core.Models;

namespace Ivy.Mailing.Core.Interfaces.Transformers
{
    /*
     * While ExtraData is maintained as a Dictionary, we may have to assign
     * other values on the base model as well.
     */
    public interface IExtraDataTransformer<TContactModel>
    {
        MailingMember Transform(MailingMember member, TContactModel contactModel);
    }
}
