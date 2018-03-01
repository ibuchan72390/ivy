using Ivy.Push.Core.Interfaces.Models.Notifications;

namespace Ivy.Push.Core.Interfaces.Models.Messages
{
    public interface INotificationPushMessage :
        IDataPushMessage
    {
        IPushNotification notification { get; set; }
    }
}
