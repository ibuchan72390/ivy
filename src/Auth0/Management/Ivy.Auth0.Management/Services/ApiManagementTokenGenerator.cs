using Ivy.Auth0.Management.Core.Models.Responses;
using Ivy.Auth0.Management.Core.Services;
using Ivy.Web.Core.Client;
using System.Threading.Tasks;

namespace Ivy.Auth0.Management.Services
{
    public class ApiManagementTokenGenerator : IApiManagementTokenGenerator
    {
        #region Variables & Constants

        private readonly IAuth0ManagementRequestGenerator _requestGenerator;
        private readonly IApiHelper _apiHelper;

        #endregion

        #region Constructor

        public ApiManagementTokenGenerator(
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
