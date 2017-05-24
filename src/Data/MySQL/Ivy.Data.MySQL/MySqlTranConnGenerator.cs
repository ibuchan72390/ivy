using System.Data;
using MySql.Data.MySqlClient;
using Ivy.Data.Common.Transaction;

namespace Ivy.Data.MySQL
{
    public class MySqlTranConnGenerator : BaseTranConnGenerator
	{
	    protected override IDbConnection InternalGetDbConnection(string connectionString)
	    {
		    return new MySqlConnection(connectionString);
	    }
	}
}
