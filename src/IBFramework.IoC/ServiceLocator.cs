using System;
using IBFramework.Core.IoC;

namespace IBFramework.IoC
{
    public class ServiceLocator : IServiceProvider, IServiceLocator
    {
        private IContainer _container;

        public IContainer Container => _container;

        public object GetService(Type serviceType)
        {
            return GetService(serviceType);
        }

        public void Init(IContainer container)
        {
            _container = container;
        }

        public void Reset()
        {
            _container = null;
        }
    }
}
