using System.Collections.Generic;

namespace Ivy.Email.Core.Domain
{
    public class SendEmailModel
    {
        #region Constructor

        public SendEmailModel()
        {
            Recipients = new List<string>();
        }

        #endregion

        #region Public Attrs

        public EmailContent Content { get; set; }
        public IEnumerable<string> Recipients { get; set; }

        #endregion
    }
}
