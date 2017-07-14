using Ivy.Auth0.Authorization.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ivy.Auth0.Authorization.Core.Models.Responses;
using Ivy.Auth0.Core.Models;
using Ivy.Web.Core.Client;

namespace Ivy.Auth0.Authorization.Services
{
    public class Auth0AuthorizationService : IAuth0AuthorizationService
    {
        #region Variables & Constants

        private readonly IAuthorizationApiTokenGenerator _tokenGen;
        private readonly IAuth0AuthorizationRequestGenerator _requestGenerator;
        private readonly IApiHelper _apiHelper;

        #endregion

        #region Constructor

        public Auth0AuthorizationService(
            IAuthorizationApiTokenGenerator tokenGen,
            IAuth0AuthorizationRequestGenerator requestGenerator,
            IApiHelper apiHelper)
        {
            _tokenGen = tokenGen;
            _requestGenerator = requestGenerator;
            _apiHelper = apiHelper;
        }

        #endregion

        #region Public Methods

        public async Task AddUserRolesAsync(string authId, IEnumerable<string> roles)
        {
            var token = await _tokenGen.GetApiTokenAsync();

            var req = _requestGenerator.GenerateAddUserRolesRequest(token, authId, roles);

            await _apiHelper.SendApiDataAsync(req);
        }

        public async Task DeleteUserRolesAsync(string authId, IEnumerable<string> roles)
        {
            var token = await _tokenGen.GetApiTokenAsync();

            var req = _requestGenerator.GenerateDeleteUserRolesRequest(token, authId, roles);

            await _apiHelper.SendApiDataAsync(req);
        }

        public async Task<Auth0RoleResponse> GetAllRolesAsync()
        {
            var token = await _tokenGen.GetApiTokenAsync();

            var req = _requestGenerator.GenerateGetRolesRequest(token);

            return await _apiHelper.GetApiDataAsync<Auth0RoleResponse>(req);
        }

        public async Task<IEnumerable<Auth0Role>> GetUserRolesAsync(string authId)
        {
            var token = await _tokenGen.GetApiTokenAsync();

            var req = _requestGenerator.GenerateGetUserRolesRequest(token, authId);

            return await _apiHelper.GetApiDataAsync<IEnumerable<Auth0Role>>(req);
        }

        #endregion
    }
}
