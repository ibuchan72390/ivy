using System;
using System.Collections.Generic;

namespace Ivy.Auth0.Core.Models
{
    public class Auth0User
    {
        #region Public Vars

        public string email { get; set; }
        public bool email_verified { get; set; }
        public string username { get; set; }
        public string phone_number { get; set; }
        public bool phone_verified { get; set; }
        public string user_id { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime created_at { get; set; }
        public IEnumerable<Auth0Identity> identities { get; set; }
        public Auth0AppMetadata app_metadata { get; set; }
        public Auth0UserMetadata user_metadata { get; set; }
        public string picture { get; set; }
        public string name { get; set; }
        public string nickname { get; set; }
        public IEnumerable<string> multifactor { get; set; }
        public string last_ip { get; set; }
        public DateTime last_login { get; set; }
        public int logins_count { get; set; }
        public bool blocked { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }

        #endregion
    }
}
