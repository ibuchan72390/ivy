using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;

namespace IBFramework.Auth0.Services
{
    public class JwtProcessor : IJwtProcessor
    {
        #region Variables & Constants

        /*
         * This thing should be simply an IClientSecretProvider so that we
         * can mock this out and remove the tie from the Auth0ConfigurationProvider
         * 
         * Then we can separate all of this stuff out into a completely separate
         * and reusable library for other projects that need JWT integration.
         */
        private readonly IAuth0ConfigurationProvider _configProvider;


        #endregion

        #region Constructor

        public JwtProcessor(
            IAuth0ConfigurationProvider configProvider)
        {
            _configProvider = configProvider;
        }


        /*
         * http://www.jerriepelser.com/blog/manually-validating-rs256-jwt-dotnet/
         * From what I understand here, this will use a remote CDN to load up some configurations for the JWT interpretation.
         * Once these configuration are loaded up, we setup validation parameters based on our Auth0 account.
         * ValidIssuer: https reference to our Auth0 domain
         * Audience: reference to the client id of the app passing the JWT
         * 
         * This creates a fully populated ClaimPrincipal containing ClaimsIdentities with the full set of interpreted JWT values.
         * If we expand the scope a little bit, we may be able to get more values out of the user here.
         */

        public async Task<ClaimsPrincipal> DecodeClaimsPrincipalAsync(string jwt)
        {
            IConfigurationManager<OpenIdConnectConfiguration> configurationManager =
                                    new ConfigurationManager<OpenIdConnectConfiguration>(
                                        $"https://{_configProvider.Domain}/.well-known/openid-configuration", 
                                        new OpenIdConnectConfigurationRetriever());

            OpenIdConnectConfiguration openIdConfig = await configurationManager.GetConfigurationAsync(CancellationToken.None);

            TokenValidationParameters validationParameters =
                new TokenValidationParameters
                {
                    ValidIssuer = $"https://{_configProvider.Domain}/",
                    ValidAudiences = new[] { _configProvider.SpaClientId },
                    IssuerSigningKeys = openIdConfig.SigningKeys
                };


            SecurityToken validatedToken;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.ValidateToken(jwt, validationParameters, out validatedToken);
        }

        #endregion
    }
}
