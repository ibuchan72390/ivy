using Ivy.IoC.Core;
using Ivy.Migration.Core.Interfaces.Providers;
using Ivy.Migration.Core.Interfaces.Services;
using Ivy.Migration.Providers;
using Ivy.Migration.Services;

namespace Ivy.Migration.IoC
{
    public class MigrationInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IDatabaseMigrationConfigurationProvider, DefaultDatabaseMigrationConfigurationProvider>();
            containerGenerator.RegisterSingleton<IDatabaseMigrationService, DatabaseMigrationService>();
            containerGenerator.RegisterSingleton<IFileAccessor, FileAccessor>();
        }
    }

    public static class MigrationInstallerExtension
    {
        public static IContainerGenerator InstallIvyMigration(this IContainerGenerator containerGenerator)
        {
            new MigrationInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
