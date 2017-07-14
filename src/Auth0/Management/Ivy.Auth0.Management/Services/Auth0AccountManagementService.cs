using Ivy.Auth0.Management.Core.Services;
using Ivy.Web.Core.Client;
using System.Threading.Tasks;

namespace Ivy.Auth0.Management.Services
{
    public class Auth0AccountManagementService : IAuth0AccountManagementService
    {
        #region Variables & Constants

        private readonly IApiHelper _apiHelper;
        private readonly IApiManagementTokenGenerator _tokenGenerator;
        private readonly IAuth0ManagementRequestGenerator _requestGenerator;

        #endregion

        #region Constructor

        public Auth0AccountManagementService(
            IApiHelper apiHelper,
            IApiManagementTokenGenerator tokenGenerator,
            IAuth0ManagementRequestGenerator requestGenerator)
        {
            _apiHelper = apiHelper;
            _tokenGenerator = tokenGenerator;
            _requestGenerator = requestGenerator;
        }

        #endregion

        #region Public Methods

        public async Task ResendVerificationEmailAsync(string userId)
        {
            var apiToken = await _tokenGenerator.GetApiAuthTokenAsync();

            var req = _requestGenerator.GenerateVerifyEmailRequest(apiToken, userId);

            await _apiHelper.SendApiDataAsync(req);
        }

        #endregion
    }
}
