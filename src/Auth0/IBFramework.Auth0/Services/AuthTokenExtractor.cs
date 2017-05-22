using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IBFramework.Auth0.Services
{
    public class AuthTokenExtractor : IAuthTokenExtractor
    {
        #region Variables & Constants

        private const string AuthorizationKey = "Authorization";

        private readonly ILogger<IAuthTokenExtractor> _logger;

        #endregion

        #region Constructor

        public AuthTokenExtractor(
            ILogger<IAuthTokenExtractor> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public string ExtractAuthToken(HttpRequest request)
        {
            // Is the auth key in the header?
            if (request.Headers.ContainsKey(AuthorizationKey))
            {
                var authHeader = request.Headers[AuthorizationKey];
                var authBits = authHeader.ToString().Split(' ');

                // Is the header valid?
                if (authBits.Length != 2)
                {
                    _logger.LogInformation("[JsonWebTokenAuthorization] Ignoring Bad Authorization Header (count!=2)");
                    return null;
                }

                if (!authBits[0].ToLowerInvariant().Equals("bearer"))
                {
                    _logger.LogInformation("[JsonWebTokenAuthorization] Ignoring Bad Authorization Header (type!=bearer)");
                    return null;
                }

                return authBits[1];
            }
            // Is the auth key in the query string?
            else if (request.Query.ContainsKey(AuthorizationKey))
            {
                return request.Query[AuthorizationKey];
            }

            // catch all
            return null;
        }

        #endregion
    }
}
