using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Management.Core.Models.Requests;
using System;

namespace Ivy.Auth0.Management.Core.Services
{
    public interface IAuth0QueryStringUriGenerator
    {
        Uri GenerateGetUsersQueryString(string currentUri, Auth0ListUsersRequest req);
    }
}
