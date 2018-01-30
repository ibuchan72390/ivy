using Ivy.Mailing.Core.Enums;
using Ivy.Mailing.MailChimp.Core.Models;
using Xunit;

namespace Ivy.Mailing.MailChimp.Test.Models
{
    public class MailChimpContactInfoTests
    {
        [Fact]
        public void MailChimpContactInfo_Defaults_To_Pending_Status()
        {
            var contact = new MailChimpContactInfo();

            Assert.Equal(MailingStatusName.Pending.ToString().ToLower(), contact.status);
        }

        [Fact]
        public void SetStatus_Properly_Lowercases_Status_Name()
        {
            const MailingStatusName status = MailingStatusName.Subscribed;

            var contact = new MailChimpContactInfo();

            contact.SetStatus(status);

            Assert.Equal(contact.status, status.ToString().ToLower());
        }
    }
}
