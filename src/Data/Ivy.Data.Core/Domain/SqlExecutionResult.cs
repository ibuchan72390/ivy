using Ivy.Data.Core.Interfaces.Sql;
using System.Collections.Generic;

namespace Ivy.Data.Core.Domain
{
    public class SqlExecutionResult : 
        ISqlExecutionResult
    {
        #region Constructor

        public SqlExecutionResult(
            string sql, 
            Dictionary<string, object> parms)
        {
            Sql = sql;
            Parms = parms;
        }

        #endregion

        #region Public Attrs

        public string Sql { get; set; }

        public Dictionary<string, object> Parms { get; set; }

        #endregion
    }
}
