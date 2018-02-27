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

            GoogleCredential googleCredential = GoogleCredential.
                FromJson(_configProvider.ServiceAccountKeyJson).
                CreateScoped(scopes);

            return await googleCredential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        }

        #endregion
    }
}
