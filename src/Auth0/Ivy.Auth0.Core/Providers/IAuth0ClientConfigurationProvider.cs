namespace Ivy.Auth0.Core.Providers
{
    public interface IAuth0ClientConfigurationProvider
    {
        // SPA Client - Used for authorizing incoming Auth0 client tokens
        // Leverages an RS256 Encrypted JWT obtained via callback
        string SpaClientId { get; }
    }
}
