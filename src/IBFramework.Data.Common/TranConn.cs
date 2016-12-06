using IBFramework.Core.Data;
using System.Data.Common;

namespace IBFramework.Data.MSSQL
{
    public class TranConn : ITranConn
    {
        public DbConnection Connection { get; set; }

        public DbTransaction Transaction { get; set; }
    }
}
