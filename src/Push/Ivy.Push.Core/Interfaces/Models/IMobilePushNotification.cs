namespace Ivy.Push.Core.Interfaces.Models
{
    public interface IMobilePushNotification : IPushNotification
    {
        string sound { get; set; }
        string body_loc_key { get; set; }
        string body_loc_args { get; set; }
        string title_loc_key { get; set; }
        string title_loc_args { get; set; }
    }
}
