using IBFramework.Core.Data;
using IBFramework.Core.Enum;
using IBFramework.Core.IoC;
using IBFramework.Data.Common;

namespace IBFramework.IoC.Installers
{
    public class CommonDataInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.Register<DatabaseKeyManager>().As<IDatabaseKeyManager>().WithLifestyle(RegistrationLifestyleType.Singleton);
            containerGenerator.Register<TranConn>().As<ITranConn>().WithLifestyle(RegistrationLifestyleType.Transient); ;
        }
    }

    public static class CommonDataInstallerExtension
    {
        public static IContainerGenerator InstallCommonData(this IContainerGenerator containerGenerator)
        {
            new CommonDataInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
