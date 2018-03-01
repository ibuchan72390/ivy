using Ivy.Push.Core.Interfaces.Models;

namespace Ivy.Push.Core.Models.Messages
{
    public class DevicePushMessage : 
        IPushMessage
    {
        public string token { get; set; }

        public IPushNotification notification { get; set; }
    }
}
