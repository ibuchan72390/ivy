using IBFramework.Core.IoC;
using IBFramework.IoC;
using IBFramework.IoC.Installers;

namespace IBFramework.TestHelper
{
    public class TestBase
    {
        protected readonly IServiceLocator ServiceLocator;

        public TestBase()
        { 
            var containerGen = new ContainerGenerator();

            // Installations
            containerGen.InstallIoC();
            containerGen.InstallCaching();

            // Generate Container
            var container = containerGen.GenerateContainer();

            // Resolve and initialize singleton ServiceLocator instance
            var svcLocator = container.Resolve<IServiceLocator>();
            svcLocator.Init(container);

            ServiceLocator = svcLocator;
        }
    }
}
