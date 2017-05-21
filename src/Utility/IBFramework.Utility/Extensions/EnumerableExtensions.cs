using System;
using System.Collections.Generic;

namespace IBFramework.Utility.Extensions
{
    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> items, Action<T> fn)
        {
            foreach (var item in items)
            {
                fn(item);
            }
        }
    }
}
