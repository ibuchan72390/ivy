using Dapper.Contrib.Extensions;
using IBFramework.TestHelper.TestEntities.Base;
using System.Collections.Generic;

namespace IBFramework.TestHelper.TestEntities
{
    [Table("guididentity")]
    public class ParentEntity : BaseTestEntity<int>
    {
        IList<CoreEntity> CoreEntities { get; set; }
    }
}
