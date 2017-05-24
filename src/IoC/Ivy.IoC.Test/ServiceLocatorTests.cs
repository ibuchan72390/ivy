using Ivy.IoC.Core;
using Ivy.TestHelper;
using Xunit;

namespace Ivy.IoC.Test
{
    public class ServiceLocatorTests
    {
        private IServiceLocator _sut;

        public ServiceLocatorTests()
        {
            _sut = new ServiceLocator();
        }

        [Fact]
        public void ServiceLocator_Can_Initialize_With_Container()
        {
            var containerGen = new ContainerGenerator();

            var container = containerGen.GenerateContainer();

            _sut.Init(container);

            Assert.NotNull(_sut.Container);
        }

        [Fact]
        public void ServiceLocator_Can_Reset_Initialized_Container()
        {
            var containerGen = new ContainerGenerator();

            var container = containerGen.GenerateContainer();

            _sut.Init(container);

            Assert.NotNull(_sut.Container);

            _sut.Reset();

            Assert.Null(_sut.Container);
        }

        [Fact]
        public void ServiceLocator_Can_Resolve_From_Initialized_Container()
        {
            var containerGen = new ContainerGenerator();

            containerGen.RegisterInstance<TestClass>(new TestClass());

            var container = containerGen.GenerateContainer();

            _sut.Init(container);

            var resolved = _sut.GetService(typeof(TestClass));

            Assert.IsType(typeof(TestClass), resolved);
        }
    }
}
