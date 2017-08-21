using Ivy.IoC.Core;
using Ivy.Utility.Core;
using Ivy.Utility.Core.Helpers;
using Ivy.Utility.Helpers;

namespace Ivy.Utility.IoC
{
    public class UtilityInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator container)
        {
            container.RegisterSingleton<IClock, Clock>();
            container.RegisterSingleton<IRandomGenerator, RandomGenerator>();
            container.RegisterSingleton<IRandomizationHelper, RandomizationHelper>();
            container.RegisterSingleton<IValidationHelper, ValidationHelper>();
        }
    }

    public static class UtilityInstallerExtension
    {
        public static IContainerGenerator InstallIvyUtility(this IContainerGenerator containerGenerator)
        {
            new UtilityInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
