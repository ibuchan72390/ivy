using Ivy.TestHelper;
using Ivy.Utility.Core.Util;
using System.Linq;
using Xunit;

namespace Ivy.Utility.Test.Util
{
    public class EnumUtilityTest
    {
        [Fact]
        public void GetEnumValues_Works_As_Expected()
        {
            var results = EnumUtility.GetValues<TestEnum>();

            Assert.Equal(4, results.Count());
            Assert.True(results.Contains(TestEnum.Test1));
            Assert.True(results.Contains(TestEnum.Test2));
            Assert.True(results.Contains(TestEnum.Test3));
            Assert.True(results.Contains(TestEnum.Test4));
        }
    }
}
