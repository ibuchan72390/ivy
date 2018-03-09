using Ivy.Push.Firebase.Core.Interfaces.Models.Messages;
using Ivy.Push.Firebase.Core.Interfaces.Models.Notifications;

namespace Ivy.Push.Firebase.Core.Models.Messages
{
    public abstract class BaseNotificationPushMessage : 
        BaseDataPushMessage,
        INotificationPushMessage
    {
        public IPushNotification notification { get; set; }
    }
}
