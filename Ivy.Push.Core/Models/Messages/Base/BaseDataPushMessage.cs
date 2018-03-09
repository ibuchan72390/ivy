using Ivy.Push.Firebase.Core.Interfaces.Models.Messages;

namespace Ivy.Push.Firebase.Core.Models.Messages
{
    public class BaseDataPushMessage :
        IDataPushMessage
    {
        public object data { get; set; }
    }
}
