using IB.Framework.Core.IoC;
using Autofac;
using System;

namespace IB.Framework.IoC
{
    public class ContainerGenerator : IContainerGenerator
    {
        private ContainerBuilder _builder;

        public ContainerGenerator()
        {
            _builder = new ContainerBuilder();
        }

        public Core.IoC.IContainer GenerateContainer()
        {
            Autofac.Core.Container container = (Autofac.Core.Container)_builder.Build();

            Container returnContainer = new Container(container);

            return returnContainer;
        }

        public IRegistrationResult Register(Type type)
        {
            var result = _builder.RegisterType(type);
            return new RegistrationResult(result);
        }

        public IRegistrationResult Register<T>()
            where T: class
        {
            var result = _builder.RegisterType(typeof(T));
            return new RegistrationResult(result);
        }

        public void RegisterInstace<T>(T instance)
            where T : class, new()
        {
            _builder.RegisterInstance(instance);
        }
    }
}
