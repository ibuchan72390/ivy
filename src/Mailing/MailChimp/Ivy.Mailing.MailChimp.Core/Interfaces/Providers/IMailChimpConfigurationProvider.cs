namespace Ivy.Mailing.MailChimp.Core.Providers
{
    public interface IMailChimpConfigurationProvider
    {
        string DataCenter { get; }

        string ApiKey { get; }

        string ListId { get; }

        // Give the consuming application customization over status messages
        string AlreadyEnrolledMessage { get; }
        string NewEnrollmentMessage { get; }
        string PendingEnrollmentMessage { get; }
    }
}
