using System;

namespace IBFramework.Core.IoC
{
    public interface IContainer
    {
        T Resolve<T>() where T : class;

        object Resolve(Type interfaceType);
    }
}
