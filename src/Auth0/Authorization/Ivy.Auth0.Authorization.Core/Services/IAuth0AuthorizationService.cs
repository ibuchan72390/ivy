using Ivy.Auth0.Authorization.Core.Models.Responses;
using Ivy.Auth0.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ivy.Auth0.Authorization.Core.Services
{
    public interface IAuth0AuthorizationService
    {
        Task<Auth0RoleResponse> GetAllRolesAsync();

        Task<IEnumerable<Auth0Role>> GetUserRolesAsync(string authId);

        Task AddUserRolesAsync(string authId, IEnumerable<string> roles);

        Task DeleteUserRolesAsync(string authId, IEnumerable<string> roles);
    }
}
