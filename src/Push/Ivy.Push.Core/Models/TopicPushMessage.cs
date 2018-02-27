using Ivy.Push.Core.Interfaces;

namespace Ivy.Push.Core.Models
{
    public class TopicPushMessage :
        IPushMessage
    {
        public string topic { get; set; }

        public PushNotification notification { get; set; }
    }
}
