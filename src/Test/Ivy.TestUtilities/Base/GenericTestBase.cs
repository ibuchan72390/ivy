using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.IoC.IoC;
using Moq;

/*
 * Examine the references on this as it develops further...
 * I don't think this is going to have an odd effect on test references at all;
 * however, if this starts heavily expanding the libarary references, we may want
 * to move this into it's own separate library for Ivy.TestHarness
 */

namespace Ivy.TestUtilities.Base
{
    public abstract class GenericTestBase
    {
        #region Variables & Constants

        // Container for reference to remove bind to static ServiceLocator
        protected readonly IContainer TestContainer;

        #endregion

        #region SetUp & TearDown

        public GenericTestBase()
        {
            // Need to new up our container really quick
            var _containerGen = new ContainerGenerator();

            // Initialize our container
            InitializeContainerFn(_containerGen);

            // Setup our local container for the time being
            TestContainer = _containerGen.GenerateContainer();

            // Setup our Service Locator for test facilities
            // This should set up our SvcLocator with the correct items for test without Mocks
            var svcLocator = TestContainer.GetService<IServiceLocator>();
            svcLocator.Init(TestContainer);
        }

        protected virtual void InitializeContainerFn(IContainerGenerator containerGen)
        {
            // We resolve the ServiceLocator in Constructor
            // May as well install this guy here while we're at it.
            containerGen.InstallIvyIoC();
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

        protected IContainerGenerator GetContainerGenerator()
        {
            return new ContainerGenerator();
        }

        #endregion
    }

    public abstract class GenericTestBase<TService> :
        GenericTestBase
        where TService : class
    {
        #region Variables & Constants

        protected readonly TService Sut;

        #endregion

        #region SetUp & TearDown

        public GenericTestBase()
        {
            Sut = TestContainer.GetService<TService>();
        }

        #endregion
    }
}
