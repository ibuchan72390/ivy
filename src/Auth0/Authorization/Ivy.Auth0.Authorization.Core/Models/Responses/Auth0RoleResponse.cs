using Ivy.Auth0.Core.Models;
using System.Collections.Generic;

namespace Ivy.Auth0.Authorization.Core.Models.Responses
{
    public class Auth0RoleResponse
    {
        #region Constructor

        public Auth0RoleResponse()
        {
            roles = new List<Auth0Role>();
        }

        #endregion

        #region Public Attributes

        public IEnumerable<Auth0Role> roles { get; set; }
        public int total { get; set; }

        #endregion
    }
}
