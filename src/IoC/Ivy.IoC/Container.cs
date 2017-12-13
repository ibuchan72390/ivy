using Ivy.IoC.Core;
using Microsoft.Extensions.Logging;
using System;

namespace Ivy.IoC
{
    public class Container : IContainer
    {
        #region Variables & Constants

        //private Autofac.Core.Container _container;
        private IServiceProvider _container;

        private ILogger<IContainer> _logger;

        #endregion

        #region Constructor

        public Container(
            IServiceProvider container)
        {
            _container = container;
        }

        public Container(
            IServiceProvider container,
            ILogger<IContainer> logger)
        {
            _container = container;

            _logger = logger;
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

            LogContainerResolve(interfaceType);

            var result = _container.GetService(interfaceType);

            return result;
        }

        public T Resolve<T>()
            where T : class
        {
            ValidateContainerInitialized();

            var resolveType = typeof(T);

            LogContainerResolve(resolveType);

            return (T)Resolve(resolveType);
        }

        public T Resolve<T>(Type interfaceType)
        {
            ValidateContainerInitialized();

            LogContainerResolve(interfaceType);

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

        private void LogContainerResolve(Type tType)
        {
            if (_logger != null)
            {
                _logger.LogDebug($"Resolving type of {tType.FullName} from IContainer");
            }
        }

        #endregion
    }
}
