using System;

namespace Ivy.IoC.Core
{
    public interface IContainer : IDisposable
    {
        T Resolve<T>() where T : class;

        object Resolve(Type interfaceType);
    }
}
