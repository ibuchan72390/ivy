namespace IBFramework.Auth0.Core.Models
{
    /*
     * used to resend Auth0 Email Verification 
     * https://auth0.com/docs/api/management/v2#!/Jobs/post_verification_email
     */

    public class Auth0VerifyEmailModel
    {
        public string user_id;
        public string client_id;
    }
}
