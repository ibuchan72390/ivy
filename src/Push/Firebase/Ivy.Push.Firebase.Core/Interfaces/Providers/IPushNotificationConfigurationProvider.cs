namespace Ivy.Push.Firebase.Core.Interfaces.Providers
{
    public interface IPushNotificationConfigurationProvider
    {
        /// <summary>
        /// The URL that we will POST our Push Messages to
        /// </summary>
        string PushUrl { get; }
    }
}
