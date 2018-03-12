using System.Threading.Tasks;
using Ivy.Push.Web.Core.Interfaces.Clients;
using WebPush;

/*
 * This should live in the implementation project because it specifically references WebPush
 * We are going to separate thsi from the Core project so that it can live without any understanding
 * of what the WebPush 3rd party library is.
 */
namespace Ivy.Push.Web.Clients
{
    public class CustomWebPushClient :
        WebPushClient,
        IWebPushClient
    {
        public void SendNotification(string endpoint, string p256dh, string auth, string payload)
        {
            var subscription = new PushSubscription
            {
                Endpoint = endpoint,
                P256DH = p256dh,
                Auth = auth
            };

            base.SendNotification(subscription, payload);
        }

        public async Task SendNotificationAsync(string endpoint, string p256dh, string auth, string payload)
        {
            var subscription = new PushSubscription
            {
                Endpoint = endpoint,
                P256DH = p256dh,
                Auth = auth
            };

            await base.SendNotificationAsync(subscription, payload);
        }
    }
}
