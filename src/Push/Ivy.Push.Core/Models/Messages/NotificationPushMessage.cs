using Ivy.Push.Core.Interfaces.Models.Messages;
using Ivy.Push.Core.Interfaces.Models.Notifications;

namespace Ivy.Push.Core.Models.Messages
{
    public class NotificationPushMessage :
        DataPushMessage,
        INotificationPushMessage
    {
        public IPushNotification notification { get; set; }
    }
}
