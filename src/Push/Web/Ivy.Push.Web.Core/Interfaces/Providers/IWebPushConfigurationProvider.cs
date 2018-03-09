namespace Ivy.Push.Web.Core.Interfaces.Providers
{
    public interface IWebPushConfigurationProvider
    {
        /// <summary>
        /// Public Key from your Vapid Encryption Keys
        /// </summary>
        string VapidPublicKey { get; }

        /// <summary>
        /// Private Key from your Vapid Encryption Keys
        /// </summary>
        string VapidPrivateKey { get; }

        /// <summary>
        /// GCM / FCM Key from your Firebase Cloud Messaging Account
        /// </summary>
        string GcmApiKey { get; }

        /// <summary>
        /// The origin of the push service.
        /// </summary>
        string PushAudience { get; }

        /// <summary>
        /// The contact Email / URL of the Push owner
        /// </summary>
        string PushSubject { get; }
    }
}
