using System.Collections.Generic;

namespace IBFramework.Data.Core.Interfaces.Domain
{
    public interface IEntityWithReferences
    {
        Dictionary<string, object> References { get; set; }
    }
}
