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
        #region Variables & Constants

        private static Random _rand;

        #endregion

        #region Public Methods

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

        public static TEnum GetRandomEnum<TEnum>()
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var enumVals = GetValues<TEnum>();

            var rand = GetPrivateRandom();

            return enumVals.ElementAt(rand.Next(enumVals.Count()));
        }

        #endregion

        #region Helper Methods

        private static Random GetPrivateRandom()
        {
            if (_rand == null)
            {
                _rand = new Random((int)DateTime.UtcNow.Ticks);
            }

            return _rand;
        }

        #endregion
    }
}
