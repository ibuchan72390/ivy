namespace Ivy.Push.Core.Interfaces.Models
{
    public interface IPushNotification
    {
        string body { get; set; }
        string title { get; set; }
        string click_action { get; set; }
    }
}
