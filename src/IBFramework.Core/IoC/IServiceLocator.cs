using System;

namespace IBFramework.Core.IoC
{
    public interface IServiceLocator : IServiceProvider
    {
        IContainer Container { get; }

        void Init(IContainer container);

        void Reset();
    }
}
