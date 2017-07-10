using Ivy.Auth0.Core.Services;
using System.Threading.Tasks;
using Ivy.Auth0.Core.Models;
using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Core.Models.Responses;
using Ivy.Web.Core.Client;

namespace Ivy.Auth0.Services
{
    public class Auth0UserManagementService : IAuth0UserManagementService
    {
        #region Variables & Constants

        private readonly IApiHelper _apiHelper;
        private readonly IUserProvider _userProvider;
        private readonly IApiAuthTokenGenerator _tokenGenerator;
        private readonly IAuth0ManagementRequestGenerator _requestGenerator;

        #endregion

        #region Constructor

        public Auth0UserManagementService(
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

        public async Task<Auth0ListUsersResponse> GetUsersAsync(Auth0ListUsersRequest request)
        {
            var apiToken = await _tokenGenerator.GetApiAuthTokenAsync();

            var req = _requestGenerator.GenerateListUsersRequest(apiToken, request);

            return await _apiHelper.GetApiDataAsync<Auth0ListUsersResponse>(req);
        }

        public async Task<Auth0User> CreateUserAsync(Auth0CreateUserRequest request)
        {
            var apiToken = await _tokenGenerator.GetApiAuthTokenAsync();

            var req = _requestGenerator.GenerateCreateUserRequest(apiToken, request);

            return await _apiHelper.GetApiDataAsync<Auth0User>(req);
        }

        public async Task<Auth0User> GetUserAsync(string userId)
        {
            var apiToken = await _tokenGenerator.GetApiAuthTokenAsync();

            var req = _requestGenerator.GenerateGetUserRequest(apiToken, userId);

            return await _apiHelper.GetApiDataAsync<Auth0User>(req);
        }

        public async Task<Auth0User> UpdateUserAsync(Auth0UpdateUserRequest request)
        {
            var apiToken = await _tokenGenerator.GetApiAuthTokenAsync();

            var req = _requestGenerator.GenerateUpdateUserRequest(apiToken, request);

            return await _apiHelper.GetApiDataAsync<Auth0User>(req);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var apiToken = await _tokenGenerator.GetApiAuthTokenAsync();

            var req = _requestGenerator.GenerateDeleteUserRequest(apiToken, userId);

            await _apiHelper.SendApiDataAsync(req);
        }

        #endregion

    }
}
