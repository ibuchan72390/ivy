namespace IBFramework.Auth0.Core.Models
{
    public class Auth0ApiTokenRequest
    {
        public string client_id;
        public string client_secret;
        public string audience;
        public string grant_type;
    }
}
