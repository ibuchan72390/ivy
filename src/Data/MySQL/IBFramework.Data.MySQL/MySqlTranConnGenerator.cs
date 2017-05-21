using System.Data;
using MySql.Data.MySqlClient;
using IBFramework.Data.Common.Transaction;

namespace IBFramework.Data.MySQL
{
    public class MySqlTranConnGenerator : BaseTranConnGenerator
	{
	    protected override IDbConnection InternalGetDbConnection(string connectionString)
	    {
		    return new MySqlConnection(connectionString);
	    }
	}
}
