using IBFramework.TestHelper.TestEntities.Base;
using System;

namespace IBFramework.TestHelper.TestEntities
{
    public class GuidIdEntity : BaseTestEntity<Guid>
    {
        CoreEntity CoreEntity { get; set; }
    }
}
