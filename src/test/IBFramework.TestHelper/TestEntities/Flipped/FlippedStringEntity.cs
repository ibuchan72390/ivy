using IBFramework.Data.Common.Attributes;
using IBFramework.TestHelper.TestEntities.Base;
using IBFramework.TestUtilities;

namespace IBFramework.TestHelper.TestEntities.Flipped
{
    public class FlippedStringEntity : BaseTestEntity<string>
    {
        // Any non-integer entities need to populate their own Id
        public FlippedStringEntity()
        {
            Id = TestIncrementer.StringVal;
        }

        [Ignore]
        public CoreEntity CoreEntity { get; set; }
    }
}
