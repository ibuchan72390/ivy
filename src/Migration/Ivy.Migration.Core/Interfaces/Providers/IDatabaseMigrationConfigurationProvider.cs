namespace Ivy.Migration.Core.Interfaces.Providers
{
    public interface IDatabaseMigrationConfigurationProvider
    {
        string LoginUserName { get; }
        string LoginPassword { get; }
    }
}
