using Ivy.Mailing.Core.Enums;
using System.Collections.Generic;

namespace Ivy.Mailing.Core.Models
{
    public class MailingMember
    {
        #region Constructor

        public MailingMember()
        {
            ExtraData = new Dictionary<string, string>();
            ListIds = new List<string>();
        }

        #endregion

        #region Public Methods

        public string Id { get; set; }

        public MailingStatusName Status { get; set; }

        // I'm assuming these 4 values make up the basic "Contact"
        // Anything extra can be recorded via the ExtraData dictionary
        // This should also cast to a JSON object very nicely for submission to API
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        // We're going to need to find out how to interpret this value
        // I really just want it to be like a raw object dictionary
        // That way we can assign custom properties like property[ExtraData[1].Key]=ExtraData[1].Value
        //public object ExtraData { get; set; }
        public Dictionary<string, string> ExtraData { get; set; }

        public IList<string> ListIds { get; set; }

        #endregion
    }
}
