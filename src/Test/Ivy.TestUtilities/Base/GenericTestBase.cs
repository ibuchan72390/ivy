using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.IoC.IoC;
using Moq;
using System;

/*
 * Examine the references on this as it develops further...
 * I don't think this is going to have an odd effect on test references at all;
 * however, if this starts heavily expanding the libarary references, we may want
 * to move this into it's own separate library for Ivy.TestHarness
 */

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
        protected abstract void TearDownFn();

        #endregion

        #region SetUp & TearDown

        public GenericTestBase()
        {
            // Need to new up our container really quick
            _containerGen = new ContainerGenerator();

            // Initialize our container
            InitializeContainerFn(_containerGen);

            // Setup our local container for the time being
            var tempContainer = _containerGen.GenerateContainer();

            // Setup our Service Locator for test facilities
            // This should set up our SvcLocator with the correct items for test without Mocks
            var svcLocator = tempContainer.GetService<IServiceLocator>();
            svcLocator.Init(tempContainer);
        }

        public void Dispose()
        {
            TearDownFn();
        }

        protected virtual void InitializeContainerFn(IContainerGenerator containerGen)
        {
            // We resolve the ServiceLocator in Constructor
            // May as well install this guy here while we're at it.
            containerGen.InstallIvyIoC();
        }

        #endregion

        #region Helper Methods

        protected virtual void InitMockingContainer(Action<IContainerGenerator> mockSetupFn)
        {
            var containerGen = new ContainerGenerator();
            InitializeContainerFn(containerGen);

            mockSetupFn(containerGen);

            InitTestContainer(containerGen);
        }

        protected virtual void InitTestContainer(IContainerGenerator containerGen = null)
        {
            if (containerGen == null) containerGen = _containerGen;

            Container = containerGen.GenerateContainer();
            Sut = Container.GetService<TService>();
        }

        protected Mock<T> InitializeMoq<T>(IContainerGenerator containerGen)
            where T : class
        {
            var mock = new Mock<T>();
            containerGen.RegisterInstance<T>(mock.Object);
            return mock;
        }

        #endregion
    }
}
