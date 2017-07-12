using Ivy.Auth0.Core.Models.Interfaces;

namespace Ivy.Auth0.Core.Services
{
    public interface IAuth0JsonManipulator
    {
        string EditPhoneJson(string json, IAuth0Phone model);
        string EditUsernameJson(string json, IAuth0Username model);
    }
}
