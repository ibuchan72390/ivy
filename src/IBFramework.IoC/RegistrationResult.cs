using System;
using Autofac.Builder;
using IB.Framework.Core.Enum;
using IB.Framework.Core.IoC;

namespace IB.Framework.IoC
{
    public class RegistrationResult : IRegistrationResult
    {
        private IRegistrationBuilder<object,
                                 ConcreteReflectionActivatorData,
                                 SingleRegistrationStyle> _result;

        public RegistrationResult(
            IRegistrationBuilder<object,
                                 ConcreteReflectionActivatorData,
                                 SingleRegistrationStyle>
            regBuilder)
        {
            _result = regBuilder;
        }

        public IRegistrationResult As(Type type)
        {
            _result.As(type);
            return this;
        }

        public IRegistrationResult As<T>()
            where T : class
        {
            _result.As<T>();
            return this;
        }

        public IRegistrationResult WithLifestyle(RegistrationLifestyleType lifestyleType)
        {
            switch (lifestyleType)
            {
                case RegistrationLifestyleType.Transient:
                    _result.InstancePerDependency();
                    break;
                case RegistrationLifestyleType.Singleton:
                    _result.SingleInstance();
                    break;
                default:
                    throw new Exception("Undefined lifestyle type");
            }

            return this;
        }
    }
}