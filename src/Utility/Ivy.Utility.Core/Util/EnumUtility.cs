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

namespace Ivy.Utility.Core.Util
{
    public static class EnumUtility
    {
        public static IEnumerable<TEnum> GetValues<TEnum>()
         where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }

        public static TEnum Parse<TEnum>(string enumText)
         where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            TEnum output;
            bool success = Enum.TryParse<TEnum>(enumText, out output);

            if (!success)
            {
                throw new Exception($"Unable to parse enum of type {typeof(TEnum).Name}. Received: {enumText}");
            }

            return output;
        }
    }
}
