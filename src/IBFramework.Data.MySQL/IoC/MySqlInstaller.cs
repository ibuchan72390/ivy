using IBFramework.Core.Data;
using IBFramework.Core.Data.SQL;
using IBFramework.Core.IoC;
using IBFramework.Data.MySQL;

namespace IBFramework.MySQL.IoC
{
    public class MySqlInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.Register<MySqlTranConnGenerator>().As<ITranConnGenerator>().WithLifestyle(Core.Enum.RegistrationLifestyleType.Transient);

            containerGenerator.Register(typeof(MySqlGenerator<>)).As(typeof(ISqlGenerator<>)).WithLifestyle(Core.Enum.RegistrationLifestyleType.Transient);
            containerGenerator.Register(typeof(MySqlGenerator<,>)).As(typeof(ISqlGenerator<,>)).WithLifestyle(Core.Enum.RegistrationLifestyleType.Transient);

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
