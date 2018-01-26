using System.Collections.Generic;

namespace Ivy.Mailing.MailChimp.Core.Models
{
    public class MailChimpList
    {
        public string id { get; set; }
        public string name { get; set; }
        public string permission_string { get; set; }
        public bool use_archive_bar { get; set; }
        public string notify_on_subscribe { get; set; }
        public string notify_on_unsubscribe { get; set; }
        public string date_created { get; set; }
        public int list_rating { get; set; }
        public bool email_type_option { get; set; }
        public string subscribe_url_short { get; set; }
        public string subscribe_url_long { get; set; }
        public string beamer_address { get; set; }
        public string visibility { get; set; }
        public IEnumerable<string> modules { get; set; }
        public MailChimpListStats stats { get; set; }
    }
}
