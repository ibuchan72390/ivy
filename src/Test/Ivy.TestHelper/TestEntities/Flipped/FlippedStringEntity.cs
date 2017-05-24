using Ivy.Data.Common.Attributes;
using Ivy.TestHelper.TestEntities.Base;
using Ivy.TestUtilities;

namespace Ivy.TestHelper.TestEntities.Flipped
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
