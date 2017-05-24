using System;
using System.Security.Cryptography;
using System.Text;

namespace Ivy.Utility.Extensions
{
    public static class StringExtensions
    {
        public static string ToMD5Hash(this string toConvert)
        {
            // byte array representation of that string
            byte[] encodedPassword = new UTF8Encoding().GetBytes(toConvert);

            // need MD5 to calculate the hash
            var md5Algo = MD5.Create();
            byte[] hash = md5Algo.ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();

            return encoded;
        }
    }
}
