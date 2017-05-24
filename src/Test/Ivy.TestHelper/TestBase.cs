using Ivy.Caching.IoC;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.IoC.IoC;
using Ivy.Utility.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ivy.TestHelper
{
    public class TestBase
    {
        #region Variables & Constants

        private Action<IContainerGenerator> _additionalContainerFns;
        private Action<IServiceCollection> _additionalServiceCollFns;

        #endregion

        #region Constructor

        public TestBase()
        {
            InitWrapper();
        }

        #endregion

        #region Public Methods

        protected virtual void InitWrapper()
        {
            Init();
        }

        protected void Init(Action<IContainerGenerator> additionalContainerFns = null, Action<IServiceCollection> additionalServiceLocatorFns = null)
        {
            var containerGen = new ContainerGenerator();

            _additionalContainerFns = additionalContainerFns;
            _additionalServiceCollFns = additionalServiceLocatorFns;

            ConfigureContainer(containerGen);

            // Generate Container
            var container = containerGen.GenerateContainer();

            // Resolve and initialize singleton ServiceLocator instance
            var svcLocator = container.Resolve<IServiceLocator>();

            svcLocator.Init(container);
        }

        protected void ConfigureContainer(IContainerGenerator containerGen)
        {
            // Installations
            containerGen.InstallIvyIoC();
            containerGen.InstallIvyCaching();
            containerGen.InstallIvyUtility();

            _additionalContainerFns?.Invoke(containerGen);
            _additionalServiceCollFns?.Invoke(containerGen.GetServiceCollection());
        }

        #endregion
    }
}
