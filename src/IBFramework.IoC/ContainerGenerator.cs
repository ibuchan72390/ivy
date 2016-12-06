using IB.Framework.Core.IoC;
using Autofac;
using Autofac.Builder;
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
            try
            {
                // Can't seem to test this without throwing an error on failure
                var x = type.GetGenericTypeDefinition();
            }
            catch
            {
                var result = _builder.RegisterType(type);
                return new RegistrationResult<ConcreteReflectionActivatorData, SingleRegistrationStyle>(result);
            }

            // Only way we can register generics is directly through type
            // Let's make sure they're registered properly
            var genericResult = _builder.RegisterGeneric(type);
            return new RegistrationResult<ReflectionActivatorData, DynamicRegistrationStyle>(genericResult);
        }

        public IRegistrationResult Register<T>()
            where T: class
        {
            var result = _builder.RegisterType(typeof(T));
            return new RegistrationResult<ConcreteReflectionActivatorData, SingleRegistrationStyle>(result);
        }

        public void RegisterInstace<T>(T instance)
            where T : class, new()
        {
            _builder.RegisterInstance(instance);
        }
    }
}
