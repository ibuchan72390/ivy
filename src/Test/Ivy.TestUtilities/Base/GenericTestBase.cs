using Ivy.IoC;
using Ivy.IoC.Core;
using Moq;
using System;

namespace Ivy.TestUtilities.Base
{
    public abstract class GenericTestBase<TTestEntity> : IDisposable
    {
        #region Variables & Constants

        // Container for reference to remove bind to static ServiceLocator
        protected readonly IContainer Container;
        protected readonly TTestEntity Sut;

        protected abstract void InitializeContainerFn(IContainerGenerator containerGen);
        protected abstract void PostSetupFn();
        protected abstract void TearDownFn();

        #endregion

        #region SetUp & TearDown

        public GenericTestBase()
        {
            // Need to new up our container really quick
            var containerGen = new ContainerGenerator();

            // Initialize our container
            InitializeContainerFn(containerGen);
            Container = containerGen.GenerateContainer();

            // Setup our Service Locator
            var svcLocator = Container.Resolve<IServiceLocator>();
            svcLocator.Init(Container);

            // Complete any remaining setups
            // Some of these may depend on a functional ServiceLocator
            PostSetupFn();
        }

        public void Dispose()
        {
            TearDownFn();
        }

        #endregion

        #region Helper Methods

        protected Mock<T> InitializeMoq<T>(IContainerGenerator containerGen)
            where T : class
        {
            var mock = new Mock<T>();
            containerGen.RegisterInstance<T>(mock);
            return mock;
        }

        protected IContainerGenerator GetMockingContainerGenerator()
        {
            var containerGen = Container.Resolve<IContainerGenerator>();
            InitializeContainerFn(containerGen);
            return containerGen;
        }

        #endregion
    }
}
