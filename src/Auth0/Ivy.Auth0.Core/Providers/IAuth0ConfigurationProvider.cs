/*
 * This will act as a sitting placeholder for a local implementation
 * We will simply mock these environmental values out of our test context.
 */

namespace Ivy.Auth0.Core.Providers
{
    public interface IAuth0ConfigurationProvider
    {
        // Auth0 Generic Account Value
        string Domain { get; }

        // Auth0 Application-Specific: Each application will have it's own users database
        string Connection { get; set; }

        // SPA Client - Used for authorizing incoming Auth0 client tokens
        // Leverages an RS256 Encrypted JWT obtained via callback
        string SpaAudience { get; }
        string SpaClientId { get; }

        // API Client - Used for interacting with the Auth0 management API
        // Leverages an RS256 Encrypted JWT obtained via POST method
        string ApiAudience { get; }
        string ApiClientId { get; }
        string ApiClientSecret { get; }
    }
}
