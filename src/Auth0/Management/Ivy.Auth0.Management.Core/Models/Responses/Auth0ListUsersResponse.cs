using Ivy.Auth0.Core.Models;
using System.Collections.Generic;

namespace Ivy.Auth0.Management.Core.Models.Responses
{
    public class Auth0ListUsersResponse
    {
        #region Constructor

        public Auth0ListUsersResponse()
        {
            users = new List<Auth0User>();
        }

        #endregion

        #region Public Vars

        public int start { get; set; }
        public int limit { get; set; }
        public int length { get; set; }
        public int total { get; set; }
        public IEnumerable<Auth0User> users { get; set; }

        #endregion
    }
}
