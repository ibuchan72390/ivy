using Ivy.Push.Core.Interfaces.Models.Messages;
using Ivy.Push.Core.Interfaces.Models.Notifications;

namespace Ivy.Push.Core.Models.Messages
{
    public abstract class BaseNotificationPushMessage : 
        BaseDataPushMessage,
        INotificationPushMessage
    {
        public IPushNotification notification { get; set; }
    }
}
