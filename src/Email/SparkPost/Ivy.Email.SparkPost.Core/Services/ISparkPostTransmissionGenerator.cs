using SparkPostDotNet.Transmissions;
using Ivy.Email.Core.Domain;

namespace Ivy.Email.SparkPost.Core.Services
{
    public interface ISparkPostTransmissionGenerator
    {
        Transmission GenerateTransmission(SendEmailModel model);
    }
}
