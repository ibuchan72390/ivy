using Ivy.Push.Core.Interfaces.Models;

namespace Ivy.Push.Core.Models.Messages
{
    public class TopicPushMessage :
        IPushMessage
    {
        public string topic { get; set; }

        public IPushNotification notification { get; set; }
    }
}
