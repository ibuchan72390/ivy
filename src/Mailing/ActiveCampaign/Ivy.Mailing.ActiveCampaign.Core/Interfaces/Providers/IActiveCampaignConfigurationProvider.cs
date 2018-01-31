/*
 * This is a custom configuration object that the consuming application must implement.
 * This will allow the consuming application to provide configurations however it wants.
 */

namespace Ivy.Mailing.ActiveCampaign.Core.Interfaces.Providers
{
    public interface IActiveCampaignConfigurationProvider
    {
        string ApiUrl { get; }
        string ApiKey { get; }
        string ListId { get; }

        string AlreadyEnrolledMessage { get; }
        string NewEnrollmentMessage { get; }
    }
}
