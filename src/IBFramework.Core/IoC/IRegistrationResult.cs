using System;
using IBFramework.Core.Enum;

namespace IBFramework.Core.IoC
{
    public interface IRegistrationResult
    {
        IRegistrationResult As<T>() where T : class;

        // Eventually will need scoped lifestyles of some kind...
        IRegistrationResult WithLifestyle(RegistrationLifestyleType lifestyleType);

        IRegistrationResult As(Type type);
    }
}