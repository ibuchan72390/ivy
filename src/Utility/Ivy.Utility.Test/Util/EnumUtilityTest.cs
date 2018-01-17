using Ivy.TestHelper;
using Ivy.Utility.Core.Util;
using System.Linq;
using Xunit;

namespace Ivy.Utility.Test.Util
{
    public class EnumUtilityTest
    {
        #region GetEnumValues

        [Fact]
        public void GetEnumValues_Works_As_Expected()
        {
            var results = EnumUtility.GetValues<TestEnum>();

            Assert.Equal(4, results.Count());
            Assert.Contains(TestEnum.Test1, results);
            Assert.Contains(TestEnum.Test2, results);
            Assert.Contains(TestEnum.Test3, results);
            Assert.Contains(TestEnum.Test4, results);
        }

        #endregion

        #region Parse

        [Fact]
        public void Parse_Executes_As_Expected()
        {
            var testEnum = TestEnum.Test1;

            var result = EnumUtility.Parse<TestEnum>(testEnum.ToString());

            Assert.Equal(testEnum, result);
        }

        #endregion

        #region GetRandomEnum

        [Fact]
        public void GetRandomEnum_Executes_As_Expected()
        {
            var allVals = EnumUtility.GetValues<TestEnum>();

            for (var i = 0; i < 10; i++)
            {
                var result = EnumUtility.GetRandomEnum<TestEnum>();
                Assert.Equal(typeof(TestEnum), result.GetType());
                Assert.Contains(result, allVals);
            }
        }

        #endregion
    }
}
