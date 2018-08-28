using System.Data;
using MySql.Data.MySqlClient;
using Ivy.Data.Common.Transaction;
using System.Threading.Tasks;

namespace Ivy.Data.MySQL
{
    public class MySqlTranConnGenerator : BaseTranConnGenerator
	{
	    protected override IDbConnection InternalGetOpenDbConnection(string connectionString)
	    {
		    var conn = new MySqlConnection(connectionString);
            conn.Open();
            return conn;
	    }

        protected override async Task<IDbConnection> InternalGetOpenDbConnectionAsync(string connectionString)
        {
            var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();
            return conn;
        }
    }
}
