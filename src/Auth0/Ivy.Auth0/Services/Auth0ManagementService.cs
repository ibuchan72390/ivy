using Ivy.Auth0.Core.Services;
using Ivy.Web.Core.Client;
using System;
using System.Threading.Tasks;

namespace Ivy.Auth0.Services
{
    public class Auth0ManagementService : IAuth0ManagementService
    {
        #region Variables & Constants

        private readonly IHttpClientHelper _clientHelper;
        private readonly IUserProvider _userProvider;
        private readonly IApiAuthTokenGenerator _tokenGenerator;
        private readonly IAuth0ManagementRequestGenerator _requestGenerator;

        #endregion

        #region Constructor

        public Auth0ManagementService(
            IHttpClientHelper clientHelper,
            IUserProvider userProvider,
            IApiAuthTokenGenerator tokenGenerator,
            IAuth0ManagementRequestGenerator requestGenerator)
        {
            _clientHelper = clientHelper;
            _userProvider = userProvider;
            _tokenGenerator = tokenGenerator;
            _requestGenerator = requestGenerator;
        }

        #endregion

        #region Public Methods

        public async Task ResendVerificationEmailAsync()
        {
            // Get our token
            var apiToken = await _tokenGenerator.GetApiAuthTokenAsync();

            var req = _requestGenerator.GenerateVerifyEmailRequest(apiToken);

            // Send our request
            var result = await _clientHelper.SendAsync(req);

            // Error out if necessary
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception($"Verification email was not successful for auth id: {_userProvider.AuthenticationId}");
            }
        }

        #endregion
    }
}
