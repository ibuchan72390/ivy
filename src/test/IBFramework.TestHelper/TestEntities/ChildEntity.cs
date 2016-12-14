using IBFramework.TestHelper.TestEntities.Base;

namespace IBFramework.TestHelper.TestEntities
{
    public class ChildEntity : BaseTestEntity<int>
    {
        CoreEntity CoreEntity { get; set; }
    }
}
