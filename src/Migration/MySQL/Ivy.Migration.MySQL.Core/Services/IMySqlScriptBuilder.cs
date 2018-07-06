using Ivy.Data.Core.Interfaces;
using MySql.Data.MySqlClient;

namespace Ivy.Migration.MySQL.Core.Services
{
    public interface IMySqlScriptBuilder
    {
        MySqlScript GenerateScript(ITranConn tc, string scriptText);
    }
}
