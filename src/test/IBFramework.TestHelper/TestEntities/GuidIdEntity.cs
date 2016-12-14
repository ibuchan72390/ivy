using IBFramework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IBFramework.TestHelper.TestEntities
{
    public class GuidIdEntity : BaseTestEntity<Guid>
    {
        CoreEntity CoreEntity { get; set; }
    }
}
