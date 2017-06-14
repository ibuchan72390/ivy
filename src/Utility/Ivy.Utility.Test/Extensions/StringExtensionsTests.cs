using Ivy.Utility.Core.Extensions;
using Xunit;

namespace Ivy.Utility.Test.Extensions
{
    public class StringExtensionsTests
    {

        /*
         * This was setup using the utility at the following url:
         * http://passwordsgenerator.net/md5-hash-generator/
         */

        [Fact]
        public void ToMD5Hash_Converts_As_Expected()
        {
            const string toConvert = "this is my test string to convert to MD5";
            const string expected = "62354AF93F59353FB703F7F98FD74A11";

            string hash = toConvert.ToMD5Hash();
            Assert.Equal(expected, hash);
        }
    }
}
