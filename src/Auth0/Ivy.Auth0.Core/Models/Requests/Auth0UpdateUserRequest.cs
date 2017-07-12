using Ivy.Auth0.Core.Models.Interfaces;

namespace Ivy.Auth0.Core.Models.Requests
{
    public class Auth0UpdateUserRequest : IAuth0Phone, IAuth0Username
    {
        public string user_id { get; set; }

        public bool blocked { get; set; }
        public bool email_verified { get; set; }
        public string email { get; set; }
        public bool verify_email { get; set; }
        public string phone_number { get; set; }
        public bool phone_verified { get; set; }
        public bool verify_phone_number { get; set; }
        public string password { get; set; }
        public bool verify_password { get; set; }
        public Auth0UserMetadata user_metadata { get; set; }
        public Auth0AppMetadata app_metadata { get; set; }
        public string connection { get; set; }
        public string username { get; set; }
        public string client_id { get; set; }
    }
}
