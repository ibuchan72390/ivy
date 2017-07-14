using System.Collections.Generic;

namespace Ivy.Auth0.Core.Models
{
    public class Auth0Role
    {
        #region Constructors

        public Auth0Role()
        {
            permissions = new List<string>();
            users = new List<string>();
        }

        #endregion

        #region Public Attributes

        public string _id { get; set; }
        public string applicationType { get; set; }
        public string applicationId { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public IEnumerable<string> permissions { get; set; }
        public IEnumerable<string> users { get; set; }

        #endregion
    }
}
