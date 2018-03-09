using Ivy.Push.Core.Interfaces.Models.Messages;

namespace Ivy.Push.Firebase.Core.Interfaces.Models.Wrappers
{
    public interface IPushMessageWrapper
    {
        // Since the DataMessage encompasses both the Data and Notification messages,
        // We'll simply set this as the base interface with the IDataMessage
        // This way, we can use either INotificationMessage or IDataMessage
        IDataPushMessage message { get; set; }
    }
}
