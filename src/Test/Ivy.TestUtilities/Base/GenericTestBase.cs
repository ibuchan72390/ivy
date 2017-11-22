using Ivy.IoC;
using Ivy.IoC.Core;
using Moq;
using System;

namespace Ivy.TestUtilities.Base
{
    public abstract class GenericTestBase<TService> : IDisposable
        where TService : class
    {
        #region Variables & Constants

        private IContainerGenerator _containerGen;

        // Container for reference to remove bind to static ServiceLocator
        protected IContainer Container;
        protected TService Sut;

        // Placeholders for custom test setup and teardown
        // Override the InitailizeContainerFn and invoke base.InitializeContainer(containerGen) to setup Mock tests
        protected abstract void InitializeContainerFn(IContainerGenerator containerGen);
        protected abstract void TearDownFn();

        #endregion

        #region SetUp & TearDown

        public GenericTestBase()
        {
            // Need to new up our container really quick
            _containerGen = new ContainerGenerator();

            // Initialize our container
            InitializeContainerFn(_containerGen);

            // Setup our Service Locator for test facilities
            var svcLocator = Container.Resolve<IServiceLocator>();
            svcLocator.Init(Container);
        }

        public void Dispose()
        {
            TearDownFn();
        }

        #endregion

        #region Helper Methods

        protected virtual void InitTestContainer(IContainerGenerator containerGen = null)
        {
            if (containerGen == null) containerGen = _containerGen;

            Container = containerGen.GenerateContainer();
            Sut = Container.Resolve<TService>();
        }

        protected Mock<T> InitializeMoq<T>(IContainerGenerator containerGen)
            where T : class
        {
            var mock = new Mock<T>();
            containerGen.RegisterInstance<T>(mock.Object);
            return mock;
        }

        protected IContainerGenerator GetMockingContainerGen()
        {
            var containerGen = new ContainerGenerator();
            InitializeContainerFn(containerGen);
            return containerGen;
        }

        #endregion
    }
}
