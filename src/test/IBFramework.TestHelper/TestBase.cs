using IBFramework.Core.Enum;
using IBFramework.Core.IoC;
using IBFramework.IoC;
using IBFramework.IoC.Installers;
using IBFramework.TestUtilities;
using System;

namespace IBFramework.TestHelper
{
    public class TestBase
    {
        public TestBase()
        {
            InitWrapper();
        }

        protected virtual void InitWrapper()
        {
            Init();
        }

        protected void Init(Action<IContainerGenerator> additionalContainerFns = null)
        {
            var containerGen = new ContainerGenerator();

            // Installations
            containerGen.InstallIoC();
            containerGen.InstallCaching();
            containerGen.InstallUtility();

            additionalContainerFns?.Invoke(containerGen);

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
