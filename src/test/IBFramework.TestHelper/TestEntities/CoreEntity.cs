using IBFramework.Data.Common.Attributes;
using IBFramework.TestHelper.TestEntities.Base;
using IBFramework.TestHelper.TestEntities.Flipped;
using System.Collections.Generic;

namespace IBFramework.TestHelper.TestEntities
{
    public class CoreEntity : BaseTestIntEntity
    {
        public ParentEntity ParentEntity { get; set; }


        //[Ignore]
        //public GuidEntity GuidIdEntity { get; set; }

        //public FlippedGuidEntity FlippedGuidEntity { get; set; }


        [Ignore]
        public StringEntity StringIdEntity { get; set; }

        public FlippedStringEntity FlippedStringEntity { get; set; }

        public IList<ChildEntity> Children { get; set; }
    }
}
