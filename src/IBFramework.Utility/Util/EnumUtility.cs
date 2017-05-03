using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBFramework.Utility.Util
{
    public static class EnumUtility
    {
        public static IEnumerable<TEnum> GetValues<TEnum>()
         where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }
    }
}
