using Ivy.Email.SparkPost.Core.Services;
using Ivy.Email.Core.Domain;
using SparkPostDotNet.Transmissions;
using System.Linq;
using Ivy.Email.Core.Services;

namespace Ivy.Email.SparkPost.Services
{
    public class SparkPostTransmissionGenerator : ISparkPostTransmissionGenerator
    {
        #region Variables & Constants

        private readonly ISendEmailModelValidator _modelValidator;

        #endregion

        #region Constructor

        public SparkPostTransmissionGenerator(
            ISendEmailModelValidator modelValidator)
        {
            _modelValidator = modelValidator;
        }

        #endregion

        #region Public Methods

        public Transmission GenerateTransmission(SendEmailModel model)
        {
            _modelValidator.Validate(model);

            var recipients = model.Recipients.
                    Select(x => new Recipient { Address = new Address { EMail = x } }).
                    ToList();

            return new Transmission
            {
                Content = new Content
                {
                    From = new Address
                    {
                        EMail = model.Content.From.Email,
                        Name = model.Content.From.Name
                    },
                    Subject = model.Content.Subject,
                    Html = model.Content.Html,

                    // I'm not sure if this needs to be populated...
                    // This is related to the spam report cycle
                    // It amy get pouplated as we run the transmission through SparkPost
                    //ReplyTo = ""
                },
                Recipients = recipients
            };
        }

        #endregion
    }
}
