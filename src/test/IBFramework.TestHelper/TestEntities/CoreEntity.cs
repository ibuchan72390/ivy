using IBFramework.Domain;
using System.Collections.Generic;

namespace IBFramework.TestHelper.TestEntities
{
    public class CoreEntity : BaseTestEntity<int>
    {
        ParentEntity ParentEntity { get; set; }

        GuidIdEntity GuidIdEntity { get; set; }
        StringIdEntity StringIdEntity { get; set; }

        IList<ChildEntity> Children { get; set; }
    }
}
