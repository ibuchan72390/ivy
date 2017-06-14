using System;
using System.Security.Cryptography;
using System.Text;

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

            return encoded.ToUpper();
        }
    }
}
