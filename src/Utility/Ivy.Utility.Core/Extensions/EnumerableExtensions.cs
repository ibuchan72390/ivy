using System;
using System.Collections.Generic;
using System.Linq;

/*
 * This lives in Core as to prevent forcing consumers to reference the implementation project.
 * 
 * It's standard to reference the Core in other projects, just makes good sense to reference the interfaces,
 * then we let the IoC container determine the implementation at runtime.
 * 
 * As such, since we reference the interfaces anyway, it makes sense for these implementation to exist here.
 * Implementations should exist in Core for PURE FUNCTIONS ONLY
 */

namespace Ivy.Utility.Core.Extensions
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

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return items != null || !items.Any();
        }
    }
}
