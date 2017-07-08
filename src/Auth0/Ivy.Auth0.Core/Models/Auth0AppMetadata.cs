using System.Collections.Generic;

namespace Ivy.Auth0.Core.Models
{
    public class Auth0AppMetadata
    {
        #region Constructor

        public Auth0AppMetadata()
        {
            roles = new List<string>();
        }

        #endregion

        #region Public Vars

        IEnumerable<string> roles { get; set; }

        #endregion
    }
}
