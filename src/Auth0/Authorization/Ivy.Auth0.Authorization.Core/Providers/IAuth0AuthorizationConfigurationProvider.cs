namespace Ivy.Auth0.Authorization.Core.Providers
{
    public interface IAuth0AuthorizationConfigurationProvider
    {
        string AuthorizationUrl { get; }
    }
}
