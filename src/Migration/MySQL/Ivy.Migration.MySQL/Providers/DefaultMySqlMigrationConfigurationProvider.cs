using Ivy.Migration.MySQL.Core.Providers;

namespace Ivy.Migration.MySQL.Providers
{
    public class DefaultMySqlMigrationConfigurationProvider :
        IMySqlMigrationConfigurationProvider
    {
        public string Delimiter => "$$";
    }
}
