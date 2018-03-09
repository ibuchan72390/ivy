namespace Ivy.Push.Web.Core.Models
{
    public class WebPushSubscription
    {
        public string endpoint { get; set; }
        public WebPushSubscriptionKeys keys { get; set; }
    }
}
