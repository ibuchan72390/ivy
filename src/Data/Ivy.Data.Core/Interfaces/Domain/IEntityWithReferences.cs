using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces.Domain
{
    public interface IEntityWithReferences
    {
        Dictionary<string, object> References { get; set; }
    }
}
