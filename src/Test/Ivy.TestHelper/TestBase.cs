using Ivy.IoC.Core;
using Ivy.TestUtilities.Base;
using Ivy.Utility.IoC;

namespace Ivy.TestHelper
{
    public class TestBase<TService> : GenericTestBase<TService>
        where TService : class
    {
        #region Constructor

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);
            containerGen.InstallIvyUtility();
        }

        #endregion

        #region Public Methods

        //protected virtual void InitWrapper()
        //{
        //    Init();
        //}

        //protected void Init(Action<IContainerGenerator> additionalContainerFns = null, Action<IServiceCollection> additionalServiceLocatorFns = null)
        //{
        //    var containerGen = new ContainerGenerator();

        //    _additionalContainerFns = additionalContainerFns;
        //    _additionalServiceCollFns = additionalServiceLocatorFns;

        //    ConfigureContainer(containerGen);

        //    // Generate Container
        //    var container = containerGen.GenerateContainer();

        //    // Resolve and initialize singleton ServiceLocator instance
        //    var svcLocator = container.GetService<IServiceLocator>();

        //    svcLocator.Init(container);
        //}

        //protected void ConfigureContainer(IContainerGenerator containerGen)
        //{
        //    // Installations
        //    containerGen.InstallIvyIoC();
        //    containerGen.InstallIvyUtility();

        //    _additionalContainerFns?.Invoke(containerGen);
        //    _additionalServiceCollFns?.Invoke(containerGen.GetServiceCollection());
        //}

        #endregion
    }
}
