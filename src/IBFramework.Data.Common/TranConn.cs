using IBFramework.Core.Data;
using System.Data.Common;

namespace IBFramework.Data.Common
{
    public class TranConn : ITranConn
    {
        public DbConnection Connection { get; set; }

        public DbTransaction Transaction { get; set; }
    }
}
