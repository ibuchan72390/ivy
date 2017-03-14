using Dapper.Contrib.Extensions;
using IBFramework.Data.Common.Attributes;
using IBFramework.TestHelper.TestEntities.Base;
using System.Collections.Generic;

namespace IBFramework.TestHelper.TestEntities
{
    public class CoreEntity : BaseTestEntity<int>
    {
        public ParentEntity ParentEntity { get; set; }

        [Ignore]
        public GuidIdEntity GuidIdEntity { get; set; }

        [Ignore]
        public StringIdEntity StringIdEntity { get; set; }

        public IList<ChildEntity> Children { get; set; }
    }
}
