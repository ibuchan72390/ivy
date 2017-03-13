using IBFramework.Core.Enum;
using IBFramework.Core.IoC;
using IBFramework.Core.Utility;
using IBFramework.Utility;

namespace IBFramework.IoC.Installers
{
    public class UtilityInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator container)
        {
            container.Register<Clock>().As<IClock>().WithLifestyle(RegistrationLifestyleType.Singleton);
            container.Register<RandomizationHelper>().As<IRandomizationHelper>().WithLifestyle(RegistrationLifestyleType.Singleton);
            container.Register<ValidationHelper>().As<IValidationHelper>().WithLifestyle(RegistrationLifestyleType.Singleton);
        }
    }

    public static class UtilityInstallerExtension
    {
        public static IContainerGenerator InstallUtility(this IContainerGenerator containerGenerator)
        {
            new UtilityInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
