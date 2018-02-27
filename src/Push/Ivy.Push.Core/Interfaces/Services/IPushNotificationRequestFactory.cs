using Ivy.Push.Core.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ivy.Push.Core.Interfaces.Services
{
    public interface IPushNotificationRequestFactory
    {
        Task<HttpRequestMessage> GeneratePushMessageRequestAsync(DevicePushMessage message);

        Task<HttpRequestMessage> GeneratePushMessageRequestAsync(TopicPushMessage message);
    }
}
