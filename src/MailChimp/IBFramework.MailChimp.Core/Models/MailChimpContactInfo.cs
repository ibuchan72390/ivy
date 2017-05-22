using IBFramework.MailChimp.Core.Enums;

namespace IBFramework.MailChimp.Core.Models
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
            status = MailChimpStatusName.pending.ToString();
        }

        #endregion

        #region Public Attributes

        public string email_address { get; set; }

        public string status { get; private set; }

        public MailChimpMergeFields merge_fields { get; set; }

        #endregion

        #region Helper Methods

        public void SetStatus(MailChimpStatusName newStatus)
        {
            status = newStatus.ToString();
        }

        #endregion
    }
}
