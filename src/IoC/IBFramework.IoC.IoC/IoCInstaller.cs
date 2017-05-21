using IBFramework.IoC.Core;

namespace IBFramework.IoC.IoC
{
    public class IoCInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator container)
        {
            container.RegisterTransient<IContainer, Container>();
            container.RegisterTransient<IContainerGenerator, ContainerGenerator>();

            container.RegisterSingleton<IServiceLocator, ServiceLocator>();
        }
    }

    public static class IoCInstallerExtension
    {
        public static IContainerGenerator InstallIoC(this IContainerGenerator containerGenerator)
        {
            new IoCInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
