using Ivy.Push.Core.Interfaces.Models;

namespace Ivy.Push.Core.Models.Notifications
{
    class ApplePushNotification :
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

        #region Apple Custom

        public string badge { get; set; }
        public string subtitle { get; set; }

        #endregion
    }
}
