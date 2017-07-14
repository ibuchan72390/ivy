/*
 * This will act as a sitting placeholder for a local implementation
 * We will simply mock these environmental values out of our test context.
 */

namespace Ivy.Auth0.Core.Providers
{
    public interface IAuth0GenericConfigurationProvider
    {
        // Auth0 Generic Account Value
        string Domain { get; }

        // Auth0 Application-Specific: Each application will have it's own users database
        string Connection { get; }
    }
}
