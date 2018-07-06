using Ivy.Migration.MySQL.Core.Services;
using MySql.Data.MySqlClient;

namespace Ivy.Migration.MySQL.Services
{
    public class MySqlScriptExecutor :
        IMySqlScriptExecutor
    {
        // This fucking method can NOT be mocked in any way
        // It's extremely frustrating and prevents me from properly testing some services
        public void Execute(MySqlScript script)
        {
            script.Execute();
        }
    }
}
