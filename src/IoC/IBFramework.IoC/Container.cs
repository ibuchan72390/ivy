using IBFramework.IoC.Core;
using System;

namespace IBFramework.IoC
{
    public class Container : IContainer
    {
        #region Variables & Constants

        //private Autofac.Core.Container _container;
        private IServiceProvider _container;

        #endregion

        #region Constructor

        public Container(IServiceProvider container)
        {
            _container = container;
        }

        #endregion

        #region Initialization & Disposal

        public void Dispose()
        {
            _container = null;
        }

        #endregion

        #region Public Methods

        public object Resolve(Type interfaceType)
        {
            ValidateContainerInitialized();

            var result = _container.GetService(interfaceType);
            return result;
        }

        public T Resolve<T>()
            where T : class
        {
            ValidateContainerInitialized();

            return (T)Resolve(typeof(T));
        }

        public T Resolve<T>(Type interfaceType)
        {
            ValidateContainerInitialized();

            var result = _container.GetService(interfaceType);
            return (T)result;
        }

        #endregion

        #region Helper Methods

        private void ValidateContainerInitialized()
        {
            if (_container == null)
                throw new Exception("Container has not been initialized!");
        }

        #endregion
    }
}
