using System.Data.Common;

namespace IBFramework.Core.Data
{
    public interface ITranConn
    {
        DbConnection Connection { get; set; }

        DbTransaction Transaction { get; set; }
    }
}
