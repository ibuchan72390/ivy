using Ivy.Auth0.Core.Services;
using Ivy.Web.Core.Client;
using System.Threading.Tasks;

namespace Ivy.Auth0.Services
{
    public class Auth0AccountManagementService : IAuth0AccountManagementService
    {
        #region Variables & Constants

        private readonly IApiHelper _apiHelper;
        private readonly IUserProvider _userProvider;
        private readonly IApiAuthTokenGenerator _tokenGenerator;
        private readonly IAuth0ManagementRequestGenerator _requestGenerator;

        #endregion

        #region Constructor

        public Auth0AccountManagementService(
            IApiHelper apiHelper,
            IUserProvider userProvider,
            IApiAuthTokenGenerator tokenGenerator,
            IAuth0ManagementRequestGenerator requestGenerator)
        {
            _apiHelper = apiHelper;
            _userProvider = userProvider;
            _tokenGenerator = tokenGenerator;
            _requestGenerator = requestGenerator;
        }

        #endregion

        #region Public Methods

        public async Task ResendVerificationEmailAsync()
        {
            var apiToken = await _tokenGenerator.GetApiAuthTokenAsync();

            var req = _requestGenerator.GenerateVerifyEmailRequest(apiToken);

            await _apiHelper.SendApiDataAsync(req);
        }

        #endregion
    }
}
