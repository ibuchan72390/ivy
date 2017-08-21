using Ivy.Email.Core.Domain;

namespace Ivy.Email.Core.Services
{
    /*
     * This should be the very first thing we do for every single email provider.
     * After we validate the model is composed correctly, we'll adjust it to the provider-specific
     * sending model and use a custom service implementation with library refs to handle it appropriately.
     * 
     * SparkPost will be the first provider we implement.  They should be relatively scalable for our needs,
     * so hopefully, this adapter pattern will not be a requirement.
     */
    public interface ISendEmailModelValidator
    {
        void Validate(SendEmailModel model);
    }
}
