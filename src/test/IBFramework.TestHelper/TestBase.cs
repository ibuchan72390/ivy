using IBFramework.Caching.IoC;
using IBFramework.IoC;
using IBFramework.IoC.Core;
using IBFramework.IoC.IoC;
using IBFramework.Utility.IoC;
using Microsoft.Extensions.DependencyInjection;
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

        protected void Init(Action<IContainerGenerator> additionalContainerFns = null, Action<IServiceCollection> additionalServiceLocatorFns = null)
        {
            var containerGen = new ContainerGenerator();

            // Installations
            containerGen.InstallIoC();
            containerGen.InstallCaching();
            containerGen.InstallUtility();

            additionalContainerFns?.Invoke(containerGen);

            additionalServiceLocatorFns?.Invoke(containerGen.GetServiceCollection());

            // Generate Container
            var container = containerGen.GenerateContainer();

            // Resolve and initialize singleton ServiceLocator instance
            var svcLocator = container.Resolve<IServiceLocator>();

            svcLocator.Init(container);
        }

    }
}
