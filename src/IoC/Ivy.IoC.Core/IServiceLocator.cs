using System;

namespace Ivy.IoC.Core
{
    public interface IServiceLocator
    {
        IContainer Container { get; }

        void Init(IContainer container);

        void Init(IServiceProvider serviceProvider);

        void Reset();
    }
}
