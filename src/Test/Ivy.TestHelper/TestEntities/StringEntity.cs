using Ivy.TestHelper.TestEntities.Base;
using Ivy.TestUtilities;

namespace Ivy.TestHelper.TestEntities
{
    public class StringEntity : BaseTestEntity<string>
    {
        // Any non-integer entities need to populate their own Id
        public StringEntity()
        {
            Id = TestIncrementer.StringVal;
        }

        public CoreEntity CoreEntity { get; set; }
    }
}
