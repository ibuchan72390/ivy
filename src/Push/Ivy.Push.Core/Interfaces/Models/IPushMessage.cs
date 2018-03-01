namespace Ivy.Push.Core.Interfaces.Models
{
    public interface IPushMessage
    {
        IPushNotification notification { get; set; }
    }
}
