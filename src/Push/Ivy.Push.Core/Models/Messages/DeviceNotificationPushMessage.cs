using Ivy.Push.Core.Interfaces.Models.Messages;

namespace Ivy.Push.Core.Models.Messages
{
    public class DeviceNotificationPushMessage : 
        BaseNotificationPushMessage,
        IDevicePushMessage
    {
        public string token { get; set; }
    }
}
