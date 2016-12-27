using System.Collections.Generic;

namespace IBFramework.Core.Utility
{
    public interface IReflectionHelper<T>
    {
        IEnumerable<string> GetPropertyNames(bool includeIgnored);
    }
}
