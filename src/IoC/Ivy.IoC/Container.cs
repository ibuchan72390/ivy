using Ivy.IoC.Core;
using Microsoft.Extensions.Logging;
using System;

namespace Ivy.IoC
{
    public class Container : IContainer
    {
        #region Variables & Constants

        private IServiceProvider _container;

        private ILogger<IContainer> _logger;

        #endregion

        #region Constructor

        public Container(
            IServiceProvider container)
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
        
        public object GetService(Type serviceType)
        {
            ValidateContainerInitialized();

            LogContainerResolve(serviceType);

            var result = _container.GetService(serviceType);

            return result;
        }

        public T GetService<T>(Type interfaceType)
        {
            var result = GetService(interfaceType);
            return (T)result;
        }

        public T GetService<T>() where T : class
        {
            var resolveType = typeof(T);
            return GetService<T>(resolveType);
        }

        public void InitializeLogger(ILogger<IContainer> logger)
        {
            _logger = logger;
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
