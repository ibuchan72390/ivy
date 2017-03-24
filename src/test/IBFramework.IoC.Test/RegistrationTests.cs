using IBFramework.Core.IoC;
using IBFramework.TestHelper;
using Xunit;

namespace IBFramework.IoC.Tests
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
            //_sut.Register<TestClass>().As<ITestInterface>();
            _sut.RegisterSingleton<ITestInterface, TestClass>();

            var container = _sut.GenerateContainer();

            var result = container.Resolve<ITestInterface>();

            Assert.IsType<TestClass>(result);
        }

        [Fact]
        public void Can_Register_To_Container_By_Type_Generate_Container_And_Resolve()
        {
            //_sut.Register(typeof(TestClass)).As(typeof(ITestInterface));
            _sut.RegisterSingleton(typeof(ITestInterface), typeof(TestClass));

            var container = _sut.GenerateContainer();

            var result = container.Resolve<ITestInterface>();

            Assert.IsType<TestClass>(result);
        }

        [Fact]
        public void Can_Register_To_Container_By_Generic_Type_Generate_Container_And_Resolve()
        {
            //_sut.Register(typeof(TestGenericClass<>)).As(typeof(ITestGenericInterface<>));
            _sut.RegisterSingleton(typeof(ITestGenericInterface<>), typeof(TestGenericClass<>));

            var container = _sut.GenerateContainer();

            var result = container.Resolve<ITestGenericInterface<int>>();

            Assert.IsType(typeof(TestGenericClass<int>), result);
        }

        [Fact]
        public void Can_Register_To_Container_By_Complex_Generic_Type_Generate_Container_And_Resolve()
        {
            //_sut.Register(typeof(TestComplexGenericClass<,>)).As(typeof(ITestComplexGenericInterface<,>));
            _sut.RegisterSingleton(typeof(ITestComplexGenericInterface<,>), typeof(TestComplexGenericClass<,>));

            var container = _sut.GenerateContainer();

            var result = container.Resolve<ITestComplexGenericInterface<int,string>>();

            Assert.IsType(typeof(TestComplexGenericClass<int,string>), result);
        }

        [Fact]
        public void Can_Register_To_Container_By_Instance_Generate_Container_And_Resolve()
        {
            var myClass = new TestClass();

            _sut.RegisterInstance(myClass);

            var container = _sut.GenerateContainer();

            var result = container.Resolve<TestClass>();

            Assert.Same(myClass, result);
        }

        [Fact]
        public void Can_Register_To_Container_By_Instance_With_Interface_Type_Generate_Container_And_Resolve()
        {
            var testClassInstance = new TestClass { Integer = 100 };

            _sut.RegisterInstance<ITestInterface, TestClass>(testClassInstance);

            var container = _sut.GenerateContainer();

            var result = container.Resolve<ITestInterface>();

            Assert.IsType<TestClass>(result);
            Assert.Equal(testClassInstance.Integer, result.Integer);
        }
    }
}
