using MySql.Data.MySqlClient;

namespace Ivy.Migration.MySQL.Core.Services
{
    public interface IMySqlScriptBuilder
    {
        MySqlScript GenerateScript(MySqlConnection conn, string scriptText);
    }
}
