using IBFramework.Data.Core.Interfaces;
using IBFramework.Data.Core.Interfaces.SQL;
using IBFramework.Data.MySQL;
using IBFramework.IoC.Core;

namespace IBFramework.MySQL.IoC
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
        public static IContainerGenerator InstallMySql(this IContainerGenerator container)
        {
            new MySqlInstaller().Install(container);
            return container;
        }
    }
}
