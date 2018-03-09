namespace Ivy.Push.Web.Core.Models
{
    public class WebPushSubscription
    {
        string endpoint { get; set; }
        WebPushSubscriptionKeys keys { get; set; }
    }
}
