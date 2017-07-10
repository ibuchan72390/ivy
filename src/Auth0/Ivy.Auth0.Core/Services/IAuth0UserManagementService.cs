using Ivy.Auth0.Core.Models;
using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Core.Models.Responses;
using System.Threading.Tasks;

namespace Ivy.Auth0.Core.Services
{
    public interface IAuth0UserManagementService
    {
        Task<Auth0ListUsersResponse> GetUsersAsync(Auth0ListUsersRequest request);

        Task<Auth0User> CreateUserAsync(Auth0CreateUserRequest request);

        Task<Auth0User> GetUserAsync(string userId);

        Task<Auth0User> UpdateUserAsync(Auth0UpdateUserRequest request);

        Task DeleteUserAsync(string userId);
    }
}
