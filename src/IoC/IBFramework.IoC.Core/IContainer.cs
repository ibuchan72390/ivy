using System;

namespace IBFramework.IoC.Core
{
    public interface IContainer : IDisposable
    {
        T Resolve<T>() where T : class;

        object Resolve(Type interfaceType);
    }
}
