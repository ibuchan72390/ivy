namespace Ivy.Mailing.MailChimp.Core.Interfaces.Models
{
    public interface IMailChimpContact
    {
        string email_address { get; set; }

        string status { get; }

        // Statically typing this guy causes way too updates to this library
        // This is also very specific to my uses, we want this for any application to use
        //public MailChimpMergeFields merge_fields { get; set; }
        object merge_fields { get; set; }
    }
}
