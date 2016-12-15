using IBFramework.Domain;
using IBFramework.TestHelper.TestEntities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBFramework.TestHelper.TestEntities
{
    public class ParentEntity : BaseTestEntity<int>
    {
        IList<CoreEntity> CoreEntities { get; set; }
    }
}
