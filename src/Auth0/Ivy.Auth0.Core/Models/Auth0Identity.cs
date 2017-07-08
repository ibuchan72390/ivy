namespace Ivy.Auth0.Core.Models
{
    public class Auth0Identity
    {
        public string connection { get; set; }
        public string user_id { get; set; }
        public string provider { get; set; }
        public bool isSocial { get; set; }
    }
}
