using System.Threading.Tasks;
using Ivy.Auth0.Core.Models;
using Ivy.Web.Core.Client;
using Ivy.Auth0.Management.Core.Services;
using Ivy.Auth0.Management.Core.Models.Responses;
using Ivy.Auth0.Management.Core.Models.Requests;
using Microsoft.Extensions.Logging;

namespace Ivy.Auth0.Management.Services
{
    public class Auth0UserManagementService : IAuth0UserManagementService
    {
        #region Variables & Constants

        private readonly IApiHelper _apiHelper;
        private readonly IManagementApiTokenGenerator _tokenGenerator;
        private readonly IAuth0ManagementRequestGenerator _requestGenerator;

        private readonly ILogger<IAuth0UserManagementService> _logger;

        #endregion

        #region Constructor

        public Auth0UserManagementService(
            IApiHelper apiHelper,
            IManagementApiTokenGenerator tokenGenerator,
            IAuth0ManagementRequestGenerator requestGenerator,
            ILogger<IAuth0UserManagementService> logger)
        {
            _apiHelper = apiHelper;
            _tokenGenerator = tokenGenerator;
            _requestGenerator = requestGenerator;

            _logger = logger;
        }

        #endregion

        #region Public Methods

        public async Task<Auth0ListUsersResponse> GetUsersAsync(Auth0ListUsersRequest request)
        {
            _logger.LogInformation("GetUsersAsync - Obtaining Token");

            var apiToken = await _tokenGenerator.GetApiTokenAsync();

            _logger.LogInformation("GetUsersAsync - Obtained Token, Generating Request");

            var req = _requestGenerator.GenerateListUsersRequest(apiToken, request);

            _logger.LogInformation("GetUsersAsync - Request Generated, Sending");

            var result = await _apiHelper.GetApiDataAsync<Auth0ListUsersResponse>(req);

            _logger.LogInformation("GetUsersAsync - Request Completed");

            return result;
        }

        public async Task<Auth0User> CreateUserAsync(Auth0CreateUserRequest request)
        {
            _logger.LogInformation("CreateUserAsync - Obtaining Token");

            var apiToken = await _tokenGenerator.GetApiTokenAsync();

            _logger.LogInformation("CreateUserAsync - Obtained Token, Generating Request");

            var req = _requestGenerator.GenerateCreateUserRequest(apiToken, request);

            _logger.LogInformation("CreateUserAsync - Request Generated, Sending");

            var result = await _apiHelper.GetApiDataAsync<Auth0User>(req);

            _logger.LogInformation("CreateUserAsync - Request Completed");

            return result;

        }

        public async Task<Auth0User> GetUserAsync(string userId)
        {
            _logger.LogInformation("GetUserAsync - Obtaining Token");

            var apiToken = await _tokenGenerator.GetApiTokenAsync();

            _logger.LogInformation("GetUserAsync - Obtained Token, Generating Request");

            var req = _requestGenerator.GenerateGetUserRequest(apiToken, userId);

            _logger.LogInformation("GetUserAsync - Request Generated, Sending");

            var result = await _apiHelper.GetApiDataAsync<Auth0User>(req);

            _logger.LogInformation("GetUserAsync - Request Completed");

            return result;

        }

        public async Task<Auth0User> UpdateUserAsync(Auth0UpdateUserRequest request)
        {
            _logger.LogInformation("UpdateUserAsync - Obtaining Token");

            var apiToken = await _tokenGenerator.GetApiTokenAsync();

            _logger.LogInformation("UpdateUserAsync - Obtained Token, Generating Request");

            var req = _requestGenerator.GenerateUpdateUserRequest(apiToken, request);

            _logger.LogInformation("UpdateUserAsync - Request Generated, Sending");

            var result = await _apiHelper.GetApiDataAsync<Auth0User>(req);

            _logger.LogInformation("UpdateUserAsync - Request Completed");

            return result;

        }

        public async Task DeleteUserAsync(string userId)
        {
            _logger.LogInformation("DeleteUserAsync - Obtaining Token");

            var apiToken = await _tokenGenerator.GetApiTokenAsync();

            _logger.LogInformation("DeleteUserAsync - Obtained Token, Generating Request");

            var req = _requestGenerator.GenerateDeleteUserRequest(apiToken, userId);

            _logger.LogInformation("DeleteUserAsync - Request Generated, Sending");

            await _apiHelper.SendApiDataAsync(req);

            _logger.LogInformation("DeleteUserAsync - Request Completed");
        }

        #endregion

    }
}
