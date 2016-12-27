using IBFramework.TestHelper.TestEntities.Base;

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
