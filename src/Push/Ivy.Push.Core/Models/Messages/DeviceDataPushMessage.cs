using Ivy.Push.Core.Interfaces.Models.Messages;

namespace Ivy.Push.Core.Models.Messages
{
    public class DeviceDataPushMessage :
        BaseDataPushMessage,
        IDevicePushMessage
    {
        public string token { get; set; }
    }
}
