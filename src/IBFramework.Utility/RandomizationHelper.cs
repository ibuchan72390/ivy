using IBFramework.Core.Utility;
using System;
using System.Text;

namespace IBFramework.Utility
{
    public class RandomizationHelper : IRandomizationHelper
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden

        public string RandomString(int size = 10)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public int RandomInt(int min = 0, int max = 10000)
        {
            return random.Next(min, max);
        }

        public double RandomDouble(double min = 0, double max = 10000)
        {
            return RandomInt((int)Math.Round(min, 0), (int)Math.Round(max - 1, 0)) + random.NextDouble();
        }

        public decimal RandomDecimal()
        {
            return (decimal)RandomDouble();
        }
    }
}
