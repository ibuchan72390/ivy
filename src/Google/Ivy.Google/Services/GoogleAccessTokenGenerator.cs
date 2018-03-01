using Ivy.Google.Core.Interfaces.Services;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Ivy.Google.Core.Interfaces.Providers;

/*
 * To be honest, I have absolutely no idea how we can feasibly test this.
 * This heavily relies on some black boxed Google Auth library methods.
 */
namespace Ivy.Google.Services
{
    public class GoogleAccessTokenGenerator : 
        IGoogleAccessTokenGenerator
    {
        #region Variables & Constants

        private readonly IGoogleConfigurationProvider _configProvider;

        #endregion

        #region Constructor

        public GoogleAccessTokenGenerator(
            IGoogleConfigurationProvider configProvider)
        {
            _configProvider = configProvider;
        }

        #endregion

        #region Public Methods

        public async Task<string> GetOAuthTokenAsync(string[] scopes)
        {
            // Each consuming project should simply provide the required scope that it needs to request
            // This way, we can use this OAuth Token Generator in many different Google API Projects

            // We may need to find a way to cache this value; however, I have no idea how to determine expiration
            // If necessary, we may need to strip the Google Library here and leverage a custom HTTP method for token exchange
            // https://developers.google.com/identity/protocols/OAuth2ServiceAccount#makingrequest
            // This appears to include information regarding the expiration of the token we receive that is not available with the library below
            // This should be placed in a cache server somewhere so we can centralize the cache between many Lambda instances

            GoogleCredential googleCredential = GoogleCredential.
                FromJson(_configProvider.ServiceAccountKeyJson).
                CreateScoped(scopes);

            return await googleCredential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        }

        #endregion
    }
}
