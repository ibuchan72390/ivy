using Ivy.Push.Core.Interfaces.Models;

namespace Ivy.Push.Core.Models.Notifications
{
    class AndroidPushNotification :
        IMobilePushNotification
    {
        #region IPushNotification

        public string body { get; set; }
        public string title { get; set; }
        public string click_action { get; set; }

        #endregion

        #region IMobilePushNotification

        public string sound { get; set; }
        public string body_loc_key { get; set; }
        public string body_loc_args { get; set; }
        public string title_loc_key { get; set; }
        public string title_loc_args { get; set; }

        #endregion

        #region Android Custom

        public string android_channel_id { get; set; }
        public string icon { get; set; }
        public string tag { get; set; }
        public string color { get; set; }

        #endregion
    }
}
