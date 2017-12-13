using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.SQL;
using Ivy.Data.MySQL;
using Ivy.IoC.Core;

namespace Ivy.MySQL.IoC
{
    public class MySqlInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterScoped<ITranConnGenerator, MySqlTranConnGenerator>();
            containerGenerator.RegisterScoped(typeof(ISqlGenerator<>), typeof(MySqlGenerator<>));
            containerGenerator.RegisterScoped(typeof(ISqlGenerator<,>), typeof(MySqlGenerator<,>));
        }
    }

    public static class MySqlInstallerExtension
    {
        public static IContainerGenerator InstallIvyMySql(this IContainerGenerator container)
        {
            new MySqlInstaller().Install(container);
            return container;
        }
    }
}
