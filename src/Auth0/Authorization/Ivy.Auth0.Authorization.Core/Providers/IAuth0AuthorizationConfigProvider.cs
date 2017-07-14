namespace Ivy.Auth0.Authorization.Core.Providers
{
    public interface IAuth0AuthorizationConfigProvider
    {
        string AuthorizationUrl { get; }
    }
}
