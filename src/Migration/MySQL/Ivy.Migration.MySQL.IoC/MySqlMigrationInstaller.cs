using Ivy.IoC.Core;
using Ivy.Migration.Core.Interfaces.Services;
using Ivy.Migration.MySQL.Core.Providers;
using Ivy.Migration.MySQL.Core.Services;
using Ivy.Migration.MySQL.Providers;
using Ivy.Migration.MySQL.Services;

namespace Ivy.Migration.MySQL.IoC
{
    public class MySqlMigrationInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IMigrationSqlExecutor, MigrationMySqlExecutor>();
            containerGenerator.RegisterSingleton<IMigrationSqlGenerator, MigrationMySqlGenerator>();
            containerGenerator.RegisterSingleton<IMySqlMigrationConfigurationProvider, DefaultMySqlMigrationConfigurationProvider>();
            containerGenerator.RegisterSingleton<IMySqlScriptBuilder, MySqlScriptBuilder>();
            containerGenerator.RegisterSingleton<IMySqlScriptExecutor, MySqlScriptExecutor>();
        }
    }

    public static class MySqlMigrationInstallerExtension
    {
        public static IContainerGenerator InstallIvyMySqlMigration(this IContainerGenerator containerGenerator)
        {
            new MySqlMigrationInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
