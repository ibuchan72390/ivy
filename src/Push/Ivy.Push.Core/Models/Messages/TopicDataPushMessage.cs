using Ivy.Push.Core.Interfaces.Models.Messages;

namespace Ivy.Push.Core.Models.Messages
{
    class TopicDataPushMessage :
        BaseDataPushMessage,
        ITopicPushMessage
    {
        public string topic { get; set; }
    }
}
