using Ivy.Push.Firebase.Core.Interfaces.Models.Messages;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ivy.Push.Firebase.Core.Interfaces.Services
{
    public interface IFirebasePushNotificationFactory
    {
        Task<HttpRequestMessage> GeneratePushMessageRequestAsync(IDataPushMessage message);
    }
}
