using Ivy.Push.Firebase.Core.Interfaces.Models.Messages;

namespace Ivy.Push.Firebase.Core.Models.Messages
{
    public class TopicNotificationPushMessage :
        BaseNotificationPushMessage,
        ITopicPushMessage
    {
        public string topic { get; set; }
    }
}
