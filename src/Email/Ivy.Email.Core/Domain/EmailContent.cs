namespace Ivy.Email.Core.Domain
{
    public class EmailContent
    {
        public EmailSender From { get; set; }
        public string Subject { get; set; }
        public string Html { get; set; }
    }
}
