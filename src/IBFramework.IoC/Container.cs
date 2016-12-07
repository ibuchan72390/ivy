using System;
using IBFramework.Core.IoC;

namespace IBFramework.IoC
{
    public class Container : IContainer
    {
        private Autofac.Core.Container _container;

        public Container(Autofac.Core.Container container)
        {
            _container = container;
        }

        public object Resolve(Type interfaceType)
        {
            var result = _container.GetService(interfaceType);
            return result;
        }

        public T Resolve<T>()
            where T : class
        {
            return (T)Resolve(typeof(T));
        }
    }
}
