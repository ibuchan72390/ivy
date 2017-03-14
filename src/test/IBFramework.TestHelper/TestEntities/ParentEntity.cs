using IBFramework.TestHelper.TestEntities.Base;
using System.Collections.Generic;

namespace IBFramework.TestHelper.TestEntities
{
    public class ParentEntity : BaseTestEntity<int>
    {
        IList<CoreEntity> CoreEntities { get; set; }
    }
}
