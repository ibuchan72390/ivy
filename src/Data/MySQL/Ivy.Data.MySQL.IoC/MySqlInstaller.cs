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
            containerGenerator.RegisterTransient<ITranConnGenerator, MySqlTranConnGenerator>();
            containerGenerator.RegisterTransient(typeof(ISqlGenerator<>), typeof(MySqlGenerator<>));
            containerGenerator.RegisterTransient(typeof(ISqlGenerator<,>), typeof(MySqlGenerator<,>));
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
