using IBFramework.TestHelper.TestEntities.Base;
using IBFramework.TestUtilities;

namespace IBFramework.TestHelper.TestEntities
{
    public class StringIdEntity : BaseTestEntity<string>
    {
        public StringIdEntity()
        {
            Id = TestIncrementer.StringVal;
        }

        public CoreEntity CoreEntity { get; set; }
    }
}
