using Microsoft.Extensions.DependencyInjection;
using System;

namespace IBFramework.Core.IoC
{
    public interface IContainerGenerator
    {
        IContainer GenerateContainer();

        // Old method, probably won't be flexible enough
        //IRegistrationResult Register<T>() where T : class;
        //IRegistrationResult Register(Type type);
        //void RegisterInstance<T>(T instance) where T : class, new();


        void RegisterSingleton(Type registrationType, Type implementationType);
        void RegisterSingleton<TInterface, TImplementation>();

        void RegisterTransient(Type registrationType, Type implementationType);
        void RegisterTransient<TInterface, TImplementation>();

        void RegisterInstance<TInterface>(object instance);
        //void RegisterInstance<T>(T instance) where T : class, new();


        // Currently untested...
        void RegisterScoped(Type registrationType, Type implementationType);
        void RegisterScoped<TInterface, TImplementation>();


        // Required to integrate with MVC stuff it seems
        IServiceCollection GetServiceCollection();



        // Desired points of expansion
        //void RegisterInstance(object instance);
        //void RegisterAssembly(Assembly assembly);
    }
}
