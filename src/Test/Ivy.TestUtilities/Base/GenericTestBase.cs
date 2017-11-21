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

        // Container for reference to remove bind to static ServiceLocator
        protected readonly IContainer Container;
        protected readonly TService Sut;

        // Placeholders for custom test setup and teardown
        // Override the InitailizeContainerFn and invoke base.InitializeContainer(containerGen) to setup Mock tests
        protected abstract void InitializeContainerFn(IContainerGenerator containerGen);
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

            Sut = Container.Resolve<TService>();

            // At this point, we can put any remaining setup in the inherited constructor
            // Constructor order of operations goes from base classes up
            // This constructor will hit first, then constructors of inherited classes will hit second.
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
            containerGen.RegisterInstance<T>(mock.Object);
            return mock;
        }

        #endregion
    }
}
