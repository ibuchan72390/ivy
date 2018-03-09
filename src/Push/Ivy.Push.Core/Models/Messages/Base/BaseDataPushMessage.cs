using Ivy.Push.Core.Interfaces.Models.Messages;

namespace Ivy.Push.Core.Models.Messages
{
    public class BaseDataPushMessage :
        IDataPushMessage
    {
        public object data { get; set; }
    }
}
