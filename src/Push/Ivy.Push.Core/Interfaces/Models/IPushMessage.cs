using Ivy.Push.Core.Interfaces.Models;

namespace Ivy.Push.Core.Models.Interfaces
{
    public interface IPushMessage
    {
        IPushNotification notification { get; set; }
    }
}
