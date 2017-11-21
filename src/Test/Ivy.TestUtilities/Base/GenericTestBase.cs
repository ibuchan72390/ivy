using Ivy.IoC;
using Ivy.IoC.Core;
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
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

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
    }
}
