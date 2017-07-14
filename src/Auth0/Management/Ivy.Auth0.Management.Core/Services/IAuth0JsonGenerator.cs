using Ivy.Auth0.Management.Core.Models.Requests;

namespace Ivy.Auth0.Management.Core.Services
{
    public interface IAuth0JsonGenerator
    {
        string ConfigureCreateUserJson(Auth0CreateUserRequest request);
        string ConfigureUpdateUserJson(Auth0UpdateUserRequest request);
    }
}
