using Ivy.Push.Core.Models;
using System.Threading.Tasks;

namespace Ivy.Push.Core.Interfaces.Services
{
    public interface IPushNotificationService
    {
        Task<PushResponse> SendPushNotificationAsync(DevicePushMessage message);

        Task<PushResponse> SendPushNotificationAsync(TopicPushMessage message);
    }
}
