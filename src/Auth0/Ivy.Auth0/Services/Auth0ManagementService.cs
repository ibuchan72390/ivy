using Ivy.Auth0.Core.Services;
using Ivy.Web.Core.Client;
using System.Threading.Tasks;
using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Core.Models.Responses;
using Ivy.Auth0.Core.Models;
using System;

namespace Ivy.Auth0.Services
{
    public class Auth0ManagementService : IAuth0ManagementService
    {
        #region Variables & Constants

        private readonly IApiHelper _apiHelper;
        private readonly IUserProvider _userProvider;
        private readonly IApiAuthTokenGenerator _tokenGenerator;
        private readonly IAuth0ManagementRequestGenerator _requestGenerator;

        #endregion

        #region Constructor

        public Auth0ManagementService(
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
