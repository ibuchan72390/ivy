using Ivy.Push.Core.Interfaces.Models.Messages;

namespace Ivy.Push.Core.Models.Messages
{
    public class DataPushMessage :
        IDataPushMessage
    {
        public object data { get; set; }
    }
}
