using IB.Framework.Core.IoC;
using IB.Framework.Core.Enum;

namespace IB.Framework.IoC.Installers
{
    public class IoCInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator container)
        {
            container.Register<Container>().As<IContainer>().WithLifestyle(RegistrationLifestyleType.Transient); ;
            container.Register<ContainerGenerator>().As<IContainerGenerator>().WithLifestyle(RegistrationLifestyleType.Transient); ;
            container.Register(typeof(RegistrationResult<,>)).As<IRegistrationResult>().WithLifestyle(RegistrationLifestyleType.Transient);

            container.Register<ServiceLocator>().As<IServiceLocator>().WithLifestyle(RegistrationLifestyleType.Singleton);
        }
    }

    public static class IoCInstallerExtension
    {
        public static void InstallIoC(this IContainerGenerator containerGenerator)
        {
            new IoCInstaller().Install(containerGenerator);
        }
    }
}
