using Ivy.Push.Core.Interfaces.Models.Messages;
using System.Threading.Tasks;

namespace Ivy.Push.Web.Core.Interfaces.Services
{
    public interface IWebPushNotificationService
    {
        void PushNotification(string endpoint, string p256dh, string auth, IDataPushMessage message);

        Task PushNotificationAsync(string endpoint, string p256dh, string auth, IDataPushMessage message);
    }
}
