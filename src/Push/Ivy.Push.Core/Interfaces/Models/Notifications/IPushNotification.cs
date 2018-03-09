namespace Ivy.Push.Core.Interfaces.Models.Notifications
{
    public interface IPushNotification
    {
        string body { get; set; }
        string title { get; set; }


        // I had originally read that this is a valid attribute;
        // however, it's invalid according to the FCM spec here:
        // https://firebase.google.com/docs/cloud-messaging/concept-options#data_messages
        //string click_action { get; set; }
    }
}
