using Ivy.Mailing.Core.Enums;

namespace Ivy.Mailing.MailChimp.Core.Models
{
    /*
     * Please keep all of the attribute names in this class as lower-case
     * When we do JsonConvert.Serialize(object), it directly maps the
     * attribute names into the JSON object.  In order to match the API
     * requirements, these attribute names need to be lower case.
     */

    public class MailChimpContactInfo
    {
        #region Constructor

        public MailChimpContactInfo()
        {
            status = MailingStatusName.Pending.ToString();
        }

        #endregion

        #region Public Attributes

        public string email_address { get; set; }

        public string status { get; private set; }

        // Statically typing this guy causes way too updates to this library
        // This is also very specific to my uses, we want this for any application to use
        //public MailChimpMergeFields merge_fields { get; set; }
        public object merge_fields { get; set; }

        #endregion

        #region Helper Methods

        public void SetStatus(MailingStatusName newStatus)
        {
            status = newStatus.ToString();
        }

        #endregion
    }
}
