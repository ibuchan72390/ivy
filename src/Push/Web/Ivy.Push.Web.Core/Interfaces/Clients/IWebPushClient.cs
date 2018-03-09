using System.Threading.Tasks;

/*
 * This client is specifically made to wrap up the WebPush.WebPushClient class
 * The class can't really be easily Mocked because it doesn't abide by an interface or anything.
 * As such, we'll create our own "CustomWebPushClient" inheriting from the WebPushClient above
 */
namespace Ivy.Push.Web.Core.Interfaces.Clients
{
    public interface IWebPushClient
    {
        void SendNotification(string endpoint, string p256dh, string auth, string payload);
        Task SendNotificationAsync(string endpoint, string p256dh, string auth, string payload);
        void SetVapidDetails(string pushSubject, string vapidPublicKey, string vapidPrivateKey);
        void SetGcmApiKey(string gcmApiKey);
    }
}
