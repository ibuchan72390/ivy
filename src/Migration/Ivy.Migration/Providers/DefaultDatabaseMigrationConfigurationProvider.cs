using Ivy.Migration.Core.Interfaces.Providers;

namespace Ivy.Migration.Providers
{
    public class DefaultDatabaseMigrationConfigurationProvider :
        IDatabaseMigrationConfigurationProvider
    {
        public string LoginUserName => "TestUser";

        public string LoginPassword => "TestPassw0rd!";
    }
}
