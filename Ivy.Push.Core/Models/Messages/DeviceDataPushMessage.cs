using Ivy.Push.Firebase.Core.Interfaces.Models.Messages;

namespace Ivy.Push.Firebase.Core.Models.Messages
{
    public class DeviceDataPushMessage :
        BaseDataPushMessage,
        IDevicePushMessage
    {
        public string token { get; set; }
    }
}
