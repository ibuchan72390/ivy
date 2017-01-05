using System.Collections.Generic;

namespace IBFramework.Core.Data.SQL
{
    public interface ISqlPropertyGenerator
    {
        IList<string> GetSqlPropertyNames<T>();
    }
}
