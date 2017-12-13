using Ivy.IoC.Core;

namespace Ivy.IoC.IoC
{
    public class IoCInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator container)
        {
            container.RegisterTransient<IContainerGenerator, ContainerGenerator>();

            container.RegisterSingleton<IContainer, Container>();
            container.RegisterSingleton<IServiceLocator, ServiceLocator>();
        }
    }

    public static class IoCInstallerExtension
    {
        public static IContainerGenerator InstallIvyIoC(this IContainerGenerator containerGenerator)
        {
            new IoCInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
