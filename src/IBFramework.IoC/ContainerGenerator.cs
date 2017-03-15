using IBFramework.Core.IoC;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace IBFramework.IoC
{
    public class ContainerGenerator : IContainerGenerator
    {
        #region Variables & Constants

        //private ContainerBuilder _builder;

        private IServiceCollection _builder;

        #endregion

        #region Constructor

        public ContainerGenerator()
        {
            //_builder = new ContainerBuilder();
        
            _builder = new ServiceCollection();
        }

        #endregion

        #region Public Methods

        #region Registration Casting

        public IContainer GenerateContainer()
        {
            return new Container(_builder.BuildServiceProvider());
        }

        public IServiceCollection GetServiceCollection()
        {
            return _builder;
        }

        #endregion

        #region Registration

        public void RegisterInstance<T>(T instance) where T : class, new()
        {
            _builder.Add(new ServiceDescriptor(typeof(T), instance));
        }

        public void RegisterSingleton(Type registrationType, Type implementationType)
        {
            _builder.Add(new ServiceDescriptor(registrationType, implementationType, ServiceLifetime.Singleton));
        }

        public void RegisterSingleton<TInterface, TImplementation>()
        {
            _builder.Add(new ServiceDescriptor(typeof(TInterface), typeof(TImplementation), ServiceLifetime.Singleton));
        }

        public void RegisterTransient(Type registrationType, Type implementationType)
        {
            _builder.Add(new ServiceDescriptor(registrationType, implementationType, ServiceLifetime.Transient));
        }

        public void RegisterTransient<TInterface, TImplementation>()
        {
            _builder.Add(new ServiceDescriptor(typeof(TInterface), typeof(TImplementation), ServiceLifetime.Transient));
        }

        public void RegisterScoped(Type registrationType, Type implementationType)
        {
            _builder.Add(new ServiceDescriptor(registrationType, implementationType, ServiceLifetime.Scoped));
        }

        public void RegisterScoped<TInterface, TImplementation>()
        {
            _builder.Add(new ServiceDescriptor(typeof(TInterface), typeof(TImplementation), ServiceLifetime.Scoped));
        }

        #endregion

        #endregion

    }
}
