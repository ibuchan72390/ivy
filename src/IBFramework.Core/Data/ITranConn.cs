using System.Data.Common;

namespace IB.Framework.Core.Data
{
    public interface ITranConn
    {
        DbConnection Connection { get; set; }

        DbTransaction Transaction { get; set; }
    }
}
