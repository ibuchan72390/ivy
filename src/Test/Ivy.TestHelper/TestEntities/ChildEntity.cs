using Ivy.TestHelper.TestEntities.Base;

namespace Ivy.TestHelper.TestEntities
{
    public class ChildEntity : BaseTestIntEntity
    {
        public CoreEntity CoreEntity { get; set; }
    }
}
