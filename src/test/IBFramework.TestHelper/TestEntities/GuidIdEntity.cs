using Dapper.Contrib.Extensions;
using IBFramework.TestHelper.TestEntities.Base;
using System;

namespace IBFramework.TestHelper.TestEntities
{
    [Table("guididentity")]
    public class GuidIdEntity : BaseTestEntity<Guid>
    {
        public GuidIdEntity()
        {
            Id = Guid.NewGuid();
        }

        public CoreEntity CoreEntity { get; set; }
    }
}
