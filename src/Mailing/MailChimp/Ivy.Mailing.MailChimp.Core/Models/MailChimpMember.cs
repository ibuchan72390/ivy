namespace Ivy.Mailing.MailChimp.Core.Models
{
    public class MailChimpMember
    {
        public string id { get; set; }

        public string email_address { get; set; }
        public string unique_email_id { get; set; }
        public string email_type { get; set; }
        public string status { get; set; }
        public string unsubscribe_reason { get; set; }

        public object merge_fields { get; set; }

        public string ip_signup { get; set; }
        public string timestamp_signup { get; set; }
        public string ip_opt { get; set; }
        public string timestamp_opt { get; set; }
        public int member_rating { get; set; }
        public string last_changed { get; set; }
        public string language { get; set; }
    }
}
