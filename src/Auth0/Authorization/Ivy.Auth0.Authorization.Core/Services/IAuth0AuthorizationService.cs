using Ivy.Auth0.Authorization.Core.Models.Responses;
using System.Collections.Generic;

namespace Ivy.Auth0.Authorization.Core.Services
{
    interface IAuth0AuthorizationService
    {
        IEnumerable<Auth0RoleResponse> GetUserRoles(string authId);
    }
}
