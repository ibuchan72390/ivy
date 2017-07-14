namespace Ivy.Auth0.Core.Providers
{
    public interface IAuth0ApiConfigurationProvider
    {
        // Generic API Variables - I don't really know if these should belong here....
        string ApiClientId { get; }
        string ApiClientSecret { get; }
    }
}
