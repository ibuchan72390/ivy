using System;
using System.Collections.Generic;
using System.Linq;

namespace Ivy.Utility.Util
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
