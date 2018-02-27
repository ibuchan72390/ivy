using Ivy.Push.Core.Models.Messages;
using Ivy.Push.Core.Models.Responses;
using System.Threading.Tasks;

namespace Ivy.Push.Core.Interfaces.Services
{
    public interface IPushNotificationService
    {
        Task<PushResponse> SendPushNotificationAsync(DevicePushMessage message);

        Task<PushResponse> SendPushNotificationAsync(TopicPushMessage message);
    }
}
