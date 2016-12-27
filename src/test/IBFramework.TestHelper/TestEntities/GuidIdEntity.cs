using IBFramework.TestHelper.TestEntities.Base;
using System;

namespace IBFramework.TestHelper.TestEntities
{
    public class GuidIdEntity : BaseTestEntity<Guid>
    {
        public GuidIdEntity()
        {
            Id = Guid.NewGuid();
        }

        public CoreEntity CoreEntity { get; set; }
    }
}
