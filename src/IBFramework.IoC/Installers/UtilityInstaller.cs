using IBFramework.Core.IoC;
using IBFramework.Core.Utility;
using IBFramework.Utility;

namespace IBFramework.IoC.Installers
{
    public class UtilityInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator container)
        {
            container.Register<Clock>().As<IClock>();
            container.Register<RandomizationHelper>().As<IRandomizationHelper>();
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
