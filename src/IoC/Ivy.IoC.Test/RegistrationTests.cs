﻿using Ivy.IoC.Core;
using Ivy.TestHelper;
using Moq;
using Xunit;

namespace Ivy.IoC.Tests
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
            _sut.RegisterSingleton<ITestInterface, TestClass>();

            var container = _sut.GenerateContainer();

            var result = container.GetService<ITestInterface>();

            Assert.IsType<TestClass>(result);
        }

        [Fact]
        public void Can_Register_To_Container_By_Type_Generate_Container_And_Resolve()
        {
            _sut.RegisterSingleton(typeof(ITestInterface), typeof(TestClass));

            var container = _sut.GenerateContainer();

            var result = container.GetService<ITestInterface>();

            Assert.IsType<TestClass>(result);
        }

        [Fact]
        public void Can_Register_To_Container_By_Generic_Type_Generate_Container_And_Resolve()
        {
            _sut.RegisterSingleton(typeof(ITestGenericInterface<>), typeof(TestGenericClass<>));

            var container = _sut.GenerateContainer();

            var result = container.GetService<ITestGenericInterface<int>>();

            Assert.IsType<TestGenericClass<int>>(result);
        }

        [Fact]
        public void Can_Register_To_Container_By_Complex_Generic_Type_Generate_Container_And_Resolve()
        {
            _sut.RegisterSingleton(typeof(ITestComplexGenericInterface<,>), typeof(TestComplexGenericClass<,>));

            var container = _sut.GenerateContainer();

            var result = container.GetService<ITestComplexGenericInterface<int,string>>();

            Assert.IsType<TestComplexGenericClass<int, string>>(result);
        }

        [Fact]
        public void Can_Register_To_Container_By_Instance_Generate_Container_And_Resolve()
        {
            var myClass = new TestClass();

            _sut.RegisterInstance<TestClass>(myClass);

            var container = _sut.GenerateContainer();

            var result = container.GetService<TestClass>();

            Assert.Same(myClass, result);
        }

        [Fact]
        public void Can_Register_To_Container_By_Instance_With_Interface_Type_Generate_Container_And_Resolve()
        {
            var testClassInstance = new TestClass { Integer = 100 };

            _sut.RegisterInstance<ITestInterface>(testClassInstance);

            var container = _sut.GenerateContainer();

            var result = container.GetService<ITestInterface>();

            Assert.IsType<TestClass>(result);
            Assert.Equal(testClassInstance.Integer, result.Integer);
        }

        [Fact]
        public void Can_Register_To_Container_By_Instance_With_Interface_Type_Generate_Container_And_Resolve_With_Moq()
        {
            const int intReturn = 100;

            var interfaceMock = new Mock<ITestInterface>();
            interfaceMock.Setup(x => x.Integer).Returns(intReturn);

            _sut.RegisterInstance<ITestInterface>(interfaceMock.Object);

            var container = _sut.GenerateContainer();

            var result = container.GetService<ITestInterface>();

            Assert.Equal(intReturn, result.Integer);

            interfaceMock.Verify(x => x.Integer, Times.Once);
        }
    }
}
