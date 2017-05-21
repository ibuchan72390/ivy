using System.Collections.Generic;

namespace IBFramework.Data.Core.Interfaces.SQL
{
    public interface ISqlPropertyGenerator
    {
        IList<string> GetSqlPropertyNames<T>();
    }
}
