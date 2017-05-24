namespace Ivy.MailChimp.Core.Models
{
    public class MailChimpError
    {
        public string type { get; set; }

        public string title { get; set; }

        public int status { get; set; }

        public string detail { get; set; }

        public string instance { get; set; }
    }
}
