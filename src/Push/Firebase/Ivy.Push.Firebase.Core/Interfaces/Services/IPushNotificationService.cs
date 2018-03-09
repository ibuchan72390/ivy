using Ivy.Push.Firebase.Core.Interfaces.Models.Messages;
using Ivy.Push.Firebase.Core.Models.Responses;
using System.Threading.Tasks;

namespace Ivy.Push.Firebase.Core.Interfaces.Services
{
    public interface IPushNotificationService
    {
        Task<PushResponse> SendPushNotificationAsync(IDataPushMessage message);
    }
}
