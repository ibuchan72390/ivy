using Ivy.Push.Firebase.Core.Interfaces.Models.Messages;

namespace Ivy.Push.Firebase.Core.Models.Messages
{
    class TopicDataPushMessage :
        BaseDataPushMessage,
        ITopicPushMessage
    {
        public string topic { get; set; }
    }
}
