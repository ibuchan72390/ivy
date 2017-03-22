using IBFramework.TestHelper.TestEntities.Base;
using IBFramework.TestUtilities;

namespace IBFramework.TestHelper.TestEntities
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
