using Ivy.TestHelper.TestEntities.Base;
using System.Collections.Generic;

namespace Ivy.TestHelper.TestEntities
{
    public class ParentEntity : BaseTestIntEntity
    {
        IList<CoreEntity> CoreEntities { get; set; }
    }
}
