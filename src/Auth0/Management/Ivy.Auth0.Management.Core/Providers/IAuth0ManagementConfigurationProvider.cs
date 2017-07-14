/*
 * Specific values for working with the Management API
 */

namespace Ivy.Auth0.Management.Core.Providers
{
    public interface IAuth0ManagementConfigurationProvider
    {
        // API Client - Used for interacting with the Auth0 management API
        // Leverages an RS256 Encrypted JWT obtained via POST method
        string ApiAudience { get; }

        // Application-Specific Processing Configurations
        bool UseUsername { get; }
    }
}
