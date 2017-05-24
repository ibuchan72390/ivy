using Ivy.Data.Common.Attributes;
using Ivy.TestHelper.TestEntities.Base;
using Ivy.TestHelper.TestEntities.Flipped;
using System.Collections.Generic;

namespace Ivy.TestHelper.TestEntities
{
    public class CoreEntity : BaseTestIntEntity
    {
        public ParentEntity ParentEntity { get; set; }

        public int WeirdAlternateIntegerId { get; set; }

        public string WeirdAlternateStringId { get; set; }

        [Ignore]
        public StringEntity StringIdEntity { get; set; }

        public FlippedStringEntity FlippedStringEntity { get; set; }

        public IList<ChildEntity> Children { get; set; }
    }
}
