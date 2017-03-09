using System.Data;
using IBFramework.Data.Common;
using MySql.Data.MySqlClient;

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
