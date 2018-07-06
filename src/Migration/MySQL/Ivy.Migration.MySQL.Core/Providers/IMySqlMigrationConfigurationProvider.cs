namespace Ivy.Migration.MySQL.Core.Providers
{
    public interface IMySqlMigrationConfigurationProvider
    {
        string Delimiter { get; }
    }
}
