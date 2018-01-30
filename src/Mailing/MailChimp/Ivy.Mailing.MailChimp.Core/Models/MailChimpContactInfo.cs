using Ivy.Mailing.Core.Enums;
using Ivy.Mailing.MailChimp.Core.Interfaces.Models;

namespace Ivy.Mailing.MailChimp.Core.Models
{
    /*
     * Please keep all of the attribute names in this class as lower-case
     * When we do JsonConvert.Serialize(object), it directly maps the
     * attribute names into the JSON object.  In order to match the API
     * requirements, these attribute names need to be lower case.
     */

    public class MailChimpContactInfo : IMailChimpContact
    {
        #region Constructor

        public MailChimpContactInfo()
        {
            SetStatus(MailingStatusName.Pending);
        }

        #endregion

        #region Public Attributes

        public string email_address { get; set; }

        // We're making this private set because this is the submission model and we MUST set it lowercase
        // This is { get; set; } on MailChimpMember because it needs to be injected to the POCO class from the JSON string
        public string status { get; private set; }

        // Statically typing this guy causes way too updates to this library
        // This is also very specific to my uses, we want this for any application to use
        //public MailChimpMergeFields merge_fields { get; set; }
        public object merge_fields { get; set; }

        #endregion

        #region Helper Methods

        public void SetStatus(MailingStatusName newStatus)
        {
            // Seems attempting to use anything but the lower case versions causes an error
            status = newStatus.ToString().ToLower();
        }

        #endregion
    }
}
