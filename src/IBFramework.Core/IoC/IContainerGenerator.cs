using System;

namespace IBFramework.Core.IoC
{
    public interface IContainerGenerator
    {
        IContainer GenerateContainer();

        IRegistrationResult Register<T>() where T : class;

        IRegistrationResult Register(Type type);

        void RegisterInstance<T>(T instance) where T : class, new();


        // Desired points of expansion
        //void RegisterInstance(object instance);
        //void RegisterAssembly(Assembly assembly);
    }
}
