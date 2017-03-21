using IBFramework.Data.Common.Attributes;
using IBFramework.TestHelper.TestEntities.Base;
using System.Collections.Generic;

namespace IBFramework.TestHelper.TestEntities
{
    public class CoreEntity : BaseTestIntEntity
    {
        public ParentEntity ParentEntity { get; set; }

        [Ignore]
        public GuidIdEntity GuidIdEntity { get; set; }

        [Ignore]
        public StringIdEntity StringIdEntity { get; set; }

        public IList<ChildEntity> Children { get; set; }
    }
}
