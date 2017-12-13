using System;

namespace Ivy.IoC.Core
{
    public interface IContainer : IDisposable, IServiceProvider
    {
        T GetService<T>() where T : class;

        T GetService<T>(Type interfaceType);
    }
}
