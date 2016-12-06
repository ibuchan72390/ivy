using IB.Framework.Core.IoC;
using IB.Framework.TestHelper;
using Xunit;

namespace IB.Framework.IoC.Tests
{
    public class RegistrationTests
    {
        private IContainerGenerator _sut;

        public RegistrationTests()
        {
            _sut = new ContainerGenerator();
        }

        [Fact]
        public void Can_Register_To_Container_By_Type_With_Generic_Method_Generate_Container_And_Resolve()
        {
            _sut.Register<TestClass>().As<ITestInterface>();

            var container = _sut.GenerateContainer();

            var result = container.Resolve<ITestInterface>();

            Assert.IsType<TestClass>(result);
        }

        [Fact]
        public void Can_Register_To_Container_By_Type_Generate_Container_And_Resolve()
        {
            _sut.Register(typeof(TestClass)).As(typeof(ITestInterface));

            var container = _sut.GenerateContainer();

            var result = container.Resolve<ITestInterface>();

            Assert.IsType<TestClass>(result);
        }

        [Fact]
        public void Can_Register_To_Container_By_Generic_Type_Generate_Container_And_Resolve()
        {
            _sut.Register(typeof(TestGenericClass<>)).As(typeof(ITestGenericInterface<>));

            var container = _sut.GenerateContainer();

            var result = container.Resolve<ITestGenericInterface<int>>();

            Assert.IsType(typeof(TestGenericClass<int>), result);
        }

        [Fact]
        public void Can_Register_To_Container_By_Complex_Generic_Type_Generate_Container_And_Resolve()
        {
            _sut.Register(typeof(TestComplexGenericClass<,>)).As(typeof(ITestComplexGenericInterface<,>));

            var container = _sut.GenerateContainer();

            var result = container.Resolve<ITestComplexGenericInterface<int,string>>();

            Assert.IsType(typeof(TestComplexGenericClass<int,string>), result);
        }

        [Fact]
        public void Can_Register_To_Container_By_Instance_Generate_Container_And_Resolve()
        {
            var myClass = new TestClass();

            _sut.RegisterInstace(myClass);

            var container = _sut.GenerateContainer();

            var result = container.Resolve<TestClass>();

            Assert.Same(myClass, result);
        }
    }
}
