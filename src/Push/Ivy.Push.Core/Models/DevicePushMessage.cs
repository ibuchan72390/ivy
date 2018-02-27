using Ivy.Push.Core.Interfaces;

namespace Ivy.Push.Core.Models
{
    public class DevicePushMessage : 
        IPushMessage
    {
        public string token { get; set; }

        public PushNotification notification { get; set; }
    }
}
