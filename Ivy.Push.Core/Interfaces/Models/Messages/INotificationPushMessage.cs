using Ivy.Push.Firebase.Core.Interfaces.Models.Notifications;

namespace Ivy.Push.Firebase.Core.Interfaces.Models.Messages
{
    public interface INotificationPushMessage :
        IDataPushMessage
    {
        IPushNotification notification { get; set; }
    }
}
