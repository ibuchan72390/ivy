using Ivy.Push.Core.Interfaces.Models.Messages;

namespace Ivy.Push.Core.Models.Messages
{
    public class TopicNotificationPushMessage :
        BaseNotificationPushMessage,
        ITopicPushMessage
    {
        public string topic { get; set; }
    }
}
