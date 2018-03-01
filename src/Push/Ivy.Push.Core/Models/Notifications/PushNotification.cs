using Ivy.Push.Core.Interfaces.Models.Notifications;

namespace Ivy.Push.Core.Models.Notifications
{
    public class PushNotification : 
        IPushNotification
    {
        public string body { get; set; }
        public string title { get; set; }
    }
}
