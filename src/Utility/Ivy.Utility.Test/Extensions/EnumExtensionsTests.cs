using Ivy.Data.Core.Domain;
using Ivy.TestUtilities.Base;
using Ivy.Utility.Core.Extensions;
using Ivy.Utility.Core.Util;
using Xunit;

namespace Ivy.Utility.Test.Extensions
{
    public class EnumExtensionsTests : GenericTestBase
    {
        #region Test Classes

        private enum TestOtherEnum
        {
            TestOtherVal1
        }

        private enum TestEnum
        {
            TestVal1,
            TestVal2,
            TestVal3
        }

        private class TestEnumEntity : EnumEntity<TestEnum>
        {
        }

        private class TestEnumEntityWithTypedId : EnumEntityWithTypedId<string, TestEnum>
        {
        }

        #endregion

        #region Tests

        #region ToEnumEntityWithTypedId

        [Fact]
        public void ToEnumEntityWithTypedId_Works_As_Expected()
        {
            foreach (var val in EnumUtility.GetValues<TestEnum>())
            {
                var result = val.ToEnumEntityWithTypedId<TestEnum, TestEnumEntityWithTypedId, string>();
                Assert.Equal(val.ToString(), result.Name);
            }
        }

        #endregion

        #region ToEnumEntity

        [Fact]
        public void ToEnumEntity_Works_As_Expected()
        {
            foreach (var val in EnumUtility.GetValues<TestEnum>())
            {
                var result = val.ToEnumEntity<TestEnum, TestEnumEntity>();
                Assert.Equal(val.ToString(), result.Name);
            }
        }

        #endregion

        #endregion
    }
}
