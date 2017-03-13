using Dapper.Contrib.Extensions;
using IBFramework.TestHelper.TestEntities.Base;

namespace IBFramework.TestHelper.TestEntities
{
    [Table("childentity")]
    public class ChildEntity : BaseTestEntity<int>
    {
        public CoreEntity CoreEntity { get; set; }
    }
}
