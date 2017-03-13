using IBFramework.Core.Data;
using IBFramework.Core.Data.SQL;
using IBFramework.Core.IoC;
using IBFramework.Data.MSSQL;

namespace IBFramework.IoC.Installers
{
    public class MsSqlInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.Register<MsSqlTranConnGenerator>().As<ITranConnGenerator>().WithLifestyle(Core.Enum.RegistrationLifestyleType.Transient);

            containerGenerator.Register(typeof(MsSqlGenerator<>)).As(typeof(ISqlGenerator<>)).WithLifestyle(Core.Enum.RegistrationLifestyleType.Transient);
            //containerGenerator.Register(typeof(MsSqlGenerator<,>)).As(typeof(ISqlGenerator<,>)).WithLifestyle(Core.Enum.RegistrationLifestyleType.Transient);
        }
    }

    public static class MsSqlInstallerExtension
    {
        public static IContainerGenerator InstallMsSql(this IContainerGenerator container)
        {
            new MsSqlInstaller().Install(container);
            return container;
        }
    }
}
