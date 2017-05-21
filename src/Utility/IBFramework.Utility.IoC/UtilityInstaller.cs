using IBFramework.IoC.Core;
using IBFramework.Utility.Core;

namespace IBFramework.Utility.IoC
{
    public class UtilityInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator container)
        {
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
