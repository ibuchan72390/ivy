using Ivy.Auth0.Management.Core.Models.Interfaces;

namespace Ivy.Auth0.Management.Core.Services
{
    public interface IAuth0JsonManipulator
    {
        string EditPhoneJson(string json, IAuth0Phone model);
        string EditUsernameJson(string json, IAuth0Username model);
    }
}
