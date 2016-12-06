using System;
using IB.Framework.Core.IoC;

namespace IB.Framework.IoC
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
            var result = Resolve(interfaceType);
            return result;
        }

        public T Resolve<T>()
            where T : class
        {
            var result =  Resolve<T>(typeof(T));
            return result;
        }

        public T Resolve<T>(Type tType)
            where T : class
        {
            var result = _container.GetService(tType);
            var massagedResult = (T)result;
            return massagedResult;
        }

        public TInterface Resolve<TInterface, TType>(TType interfaceType)
            where TInterface : TType
            where TType : Type
        {
            return (TInterface)_container.GetService(interfaceType);
        }
    }
}
