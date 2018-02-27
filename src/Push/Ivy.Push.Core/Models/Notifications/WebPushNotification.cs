using Ivy.Push.Core.Interfaces.Models;

namespace Ivy.Push.Core.Models.Notifications
{
    public class WebPushNotification : 
        IPushNotification
    {
        public string body { get; set; }
        public string title { get; set; }
        public string click_action { get; set; }
    }
}
