using Ivy.IoC.Core;
using System;

namespace Ivy.IoC
{
    public class ServiceLocator : IServiceLocator
    {
        #region Variables & Constants

        private static IContainer _container;

        public virtual IContainer Container => _container;

        // Do NOT use when possible!!!
        // This is only for use when a proper constructor injector pattern is not feasible
        public static IContainer Instance
        {
            get
            {
                if (_container == null)
                    throw new Exception("ServiceLocator not Initialized!");

                return _container;
            }
        }

        #endregion

        #region Public Methods

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public virtual void Reset()
        {
            _container = null;
        }

        #region Initialization

        public virtual void Init(IContainer container)
        {
            _container = container;
        }

        public virtual void Init(IServiceProvider serviceProvider)
        {
            _container = new Container(serviceProvider);
        }

        #endregion

        #endregion
    }
}
