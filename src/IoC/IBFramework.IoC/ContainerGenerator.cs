﻿using System;
using Microsoft.Extensions.DependencyInjection;
using IBFramework.IoC.Core;

namespace IBFramework.IoC
{
    public class ContainerGenerator : IContainerGenerator
    {
        #region Variables & Constants

        private IServiceCollection _builder;

        #endregion

        #region Constructor

        public ContainerGenerator()
        {
            _builder = new ServiceCollection();
        }

        public ContainerGenerator(IServiceCollection serviceCollection)
        {
            _builder = serviceCollection;
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

        // Previous attempt - should NOT be generic, the generic types don't work well with the Moq library
        //public void RegisterInstance<T>(T instance) where T : class, new()
        //{
        //    _builder.Add(new ServiceDescriptor(typeof(T), instance));
        //}

        public void RegisterInstance<TInterface>(object instance)
        {
            _builder.Add(new ServiceDescriptor(typeof(TInterface), instance));
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
