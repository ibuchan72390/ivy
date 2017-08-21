using Ivy.Utility.Core;
using Ivy.Utility.Core.Helpers;
using System;
using System.Text;

namespace Ivy.Utility
{
    public class RandomizationHelper : IRandomizationHelper
    {
        #region Variables & Constants

        private readonly Random _random;

        #endregion

        #region Constructor

        public RandomizationHelper(
            IRandomGenerator randomGen)
        {
            _random = randomGen.GetRandom();
        }

        #endregion

        #region Public Methods

        public string RandomString(int size = 10)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * _random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public int RandomInt(int min = 0, int max = 10000)
        {
            return _random.Next(min, max + 1);
        }

        public double RandomDouble(double min = 0, double max = 10000)
        {
            return RandomInt((int)Math.Round(min, 0), (int)Math.Round(max - 1, 0)) + _random.NextDouble();
        }

        public decimal RandomDecimal()
        {
            return (decimal)RandomDouble();
        }

        public bool RandomBool()
        {
            var result = RandomInt(0, 1);
            return result == 0;
        }

        #endregion
    }
}
