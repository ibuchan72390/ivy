using IBFramework.Core.Enum;
using IBFramework.Core.IoC;
using IBFramework.IoC;
using IBFramework.IoC.Installers;

namespace IBFramework.TestHelper
{
    public class TestBase
    {
        public TestBase()
        { 
            var containerGen = new ContainerGenerator();

            // Installations
            containerGen.InstallIoC();
            containerGen.InstallCaching();
            containerGen.InstallUtility();

            // Attempt to override
            containerGen.Register<TestServiceLocator>().As<IServiceLocator>().WithLifestyle(RegistrationLifestyleType.Singleton);

            // Generate Container
            var container = containerGen.GenerateContainer();

            // Resolve and initialize singleton ServiceLocator instance
            var svcLocator = container.Resolve<IServiceLocator>();
            svcLocator.Init(container);
        }
    }
}
