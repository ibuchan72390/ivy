using System;
using IB.Framework.Core.Enum;

namespace IB.Framework.Core.IoC
{
    public interface IRegistrationResult
    {
        IRegistrationResult As<T>() where T : class;

        // Eventually will need scoped lifestyles of some kind...
        IRegistrationResult WithLifestyle(RegistrationLifestyleType lifestyleType);

        IRegistrationResult As(Type type);
    }
}