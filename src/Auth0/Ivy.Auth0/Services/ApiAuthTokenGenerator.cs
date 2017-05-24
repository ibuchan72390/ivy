using Ivy.Auth0.Core.Models;
using Ivy.Auth0.Core.Services;
using Ivy.Web.Core.Client;
using System.Threading.Tasks;

namespace Ivy.Auth0.Services
{
    public class ApiAuthTokenGenerator : IApiAuthTokenGenerator
    {
        #region Variables & Constants

        private readonly IAuth0ManagementRequestGenerator _requestGenerator;
        private readonly IApiHelper _apiHelper;

        #endregion

        #region Constructor

        public ApiAuthTokenGenerator(
            IAuth0ManagementRequestGenerator requestGenerator,
            IApiHelper apiHelper)
        {
            _requestGenerator = requestGenerator;
            _apiHelper = apiHelper;
        }

        #endregion

        #region Public Methods

        public async Task<string> GetApiAuthTokenAsync()
        {
            var req = _requestGenerator.GenerateManagementApiTokenRequest();

            var responseModel = await _apiHelper.GetApiDataAsync<Auth0AccessTokenResponse>(req);

            return responseModel.access_token;
        }

        #endregion
    }
}
