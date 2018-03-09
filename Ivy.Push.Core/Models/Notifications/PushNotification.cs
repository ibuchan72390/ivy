using Ivy.Push.Firebase.Core.Interfaces.Models.Notifications;

namespace Ivy.Push.Firebase.Core.Models.Notifications
{
    public class PushNotification : 
        IPushNotification
    {
        public string body { get; set; }
        public string title { get; set; }
    }
}
