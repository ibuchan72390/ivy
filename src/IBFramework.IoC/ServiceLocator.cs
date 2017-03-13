using System;
using IBFramework.Core.IoC;

namespace IBFramework.IoC
{
    public class ServiceLocator : IServiceLocator
    {
        private static IContainer _container;

        public virtual IContainer Container => _container;

        public static IContainer Instance
        {
            get
            {
                if (_container == null)
                    throw new Exception("ServiceLocator not Initialized!");

                return _container;
            }
        }   

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public virtual void Init(IContainer container)
        {
            _container = container;
        }

        public virtual void Reset()
        {
            _container = null;
        }
    }
}
