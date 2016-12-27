using System;
using System.Collections.Generic;
using IBFramework.Core.Utility;

namespace IBFramework.Utility
{
    public class ReflectionHelper<T> : IReflectionHelper<T>
    {
        private readonly Type _type;

        public ReflectionHelper()
        {
            _type = typeof(T);
        }

        public IEnumerable<string> GetPropertyNames(bool includeIgnored)
        {
            // WTF, how do i get prop names???
            //_type.
            throw new NotImplementedException();
        }
    }
}
