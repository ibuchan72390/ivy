using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces.Sql
{
    public interface ISqlExecutionResult
    {
        string Sql { get; set; }

        Dictionary<string, object> Parms { get; set; }
    }
}
