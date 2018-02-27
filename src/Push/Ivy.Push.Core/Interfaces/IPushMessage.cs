using Ivy.Push.Core.Models;

namespace Ivy.Push.Core.Interfaces
{
    public interface IPushMessage
    {
        PushNotification notification { get; set; }
    }
}
