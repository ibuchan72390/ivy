using System;
using Autofac.Builder;
using IBFramework.Core.Enum;
using IBFramework.Core.IoC;

namespace IBFramework.IoC
{
    public class RegistrationResult<TActivatorData, TRegistrationStyle> : IRegistrationResult
    {
        private IRegistrationBuilder<object,
                                 TActivatorData,
                                 TRegistrationStyle> _result;

        public RegistrationResult(
            IRegistrationBuilder<object,
                                 TActivatorData,
                                 TRegistrationStyle>
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