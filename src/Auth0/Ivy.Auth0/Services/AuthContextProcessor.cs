using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Ivy.Auth0.Core.Services;
using Microsoft.Extensions.Logging;

namespace Ivy.Auth0.Services
{
    public class Auth0ContextProcessor : IAuth0ContextProcessor
    {
        #region Variables & Constants

        private readonly IJwtProcessor _jwtProcessor;
        private readonly IAuthTokenExtractor _authTokenExtractor;

        private readonly ILogger<IAuth0ContextProcessor> _logger;

        #endregion

        #region Constructor

        public Auth0ContextProcessor(
            IJwtProcessor jwtProcessor,
            IAuthTokenExtractor authTokenExtractor,
            ILogger<IAuth0ContextProcessor> logger)
        {
            _jwtProcessor = jwtProcessor;
            _authTokenExtractor = authTokenExtractor;

            _logger = logger;
        }

        #endregion

        #region Public Methods

        public async Task ProcessContextAsync(HttpContext context)
        {
            string jwt = _authTokenExtractor.ExtractAuthToken(context.Request);

            if (jwt != null)
            {
                try
                {
                    var user = await _jwtProcessor.DecodeClaimsPrincipalAsync(jwt);
                    context.User = user;

                    _logger.LogDebug("User is authorized!");
                }
                catch (Exception e)
                {
                    _logger.LogError($"An exception has occurred while procesing the JWT. Message: {e.Message}");
                    return;
                }
            }

            _logger.LogDebug("In JsonWebTokenAuthorization.Invoke");
        }

        #endregion
    }
}
