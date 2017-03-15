using IBFramework.Core.IoC;
using IBFramework.Core.Utility;

namespace IBFramework.Utility.IoC
{
    public class UtilityInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator container)
        {
            //container.Register<Clock>().As<IClock>().WithLifestyle(RegistrationLifestyleType.Singleton);
            //container.Register<RandomizationHelper>().As<IRandomizationHelper>().WithLifestyle(RegistrationLifestyleType.Singleton);
            //container.Register<ValidationHelper>().As<IValidationHelper>().WithLifestyle(RegistrationLifestyleType.Singleton);

            container.RegisterSingleton<IClock, Clock>();
            container.RegisterSingleton<IRandomizationHelper, RandomizationHelper>();
            container.RegisterSingleton<IValidationHelper, ValidationHelper>();
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
