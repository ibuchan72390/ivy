using IBFramework.TestHelper.TestEntities.Base;

namespace IBFramework.TestHelper.TestEntities
{
    public class ChildEntity : BaseTestIntEntity
    {
        public CoreEntity CoreEntity { get; set; }
    }
}
