using Ivy.Data.Common.Transaction;
using System.Data;
using System.Data.SqlClient;

namespace Ivy.Data.MSSQL
{
    public class MsSqlTranConnGenerator : BaseTranConnGenerator
    {
        protected override IDbConnection InternalGetDbConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}