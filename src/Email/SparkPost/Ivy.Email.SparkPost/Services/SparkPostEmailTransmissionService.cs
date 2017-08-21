using Ivy.Email.Core.Services;
using System.Threading.Tasks;
using Ivy.Email.Core.Domain;
using Ivy.Email.SparkPost.Core.Services;
using SparkPostDotNet;

namespace Ivy.Email.SparkPost.Services
{
    public class SparkPostEmailTransmissionService : IEmailTransmissionService
    {
        #region Variables & Constants

        private readonly ISparkPostTransmissionGenerator _transmissionGenerator;

        private readonly SparkPostClient _client;

        #endregion

        #region Constructor
        
        public SparkPostEmailTransmissionService(
            ISparkPostTransmissionGenerator transmissionGenerator,
            
            // The fact that he's registering an instance over an interface is fucking absurd
            SparkPostClient client)
        {
            _transmissionGenerator = transmissionGenerator;

            _client = client;
        }
        
        #endregion

        #region Public Methods

        public async Task SendEmailAsync(SendEmailModel model)
        {
            var spTransmission = _transmissionGenerator.GenerateTransmission(model);

            await _client.CreateTransmission(spTransmission);
        }

        #endregion
    }
}
