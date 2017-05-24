using System.Collections.Generic;

namespace Ivy.Data.Core.Interfaces.SQL
{
    public interface ISqlPropertyGenerator
    {
        IList<string> GetSqlPropertyNames<T>();
    }
}
