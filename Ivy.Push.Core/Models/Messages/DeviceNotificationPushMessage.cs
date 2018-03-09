using Ivy.Push.Firebase.Core.Interfaces.Models.Messages;

namespace Ivy.Push.Firebase.Core.Models.Messages
{
    public class DeviceNotificationPushMessage : 
        BaseNotificationPushMessage,
        IDevicePushMessage
    {
        public string token { get; set; }
    }
}
