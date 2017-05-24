using Ivy.TestHelper;
using Ivy.TestHelper.TestEntities;
using Xunit;

namespace Ivy.Data.Common.Test
{
    public class EnumEntityTests
    {
        [Fact]
        public void GetEnumValue_Returns_Cast_Value_Of_Name_Attribute()
        {
            const TestEnum testVal = TestEnum.Test4;

            var testEnum = new TestEnumEntity { Name = testVal.ToString() }.GenerateForTest();

            Assert.Equal(testVal, testEnum.GetEnumValue());
        }
    }
}
