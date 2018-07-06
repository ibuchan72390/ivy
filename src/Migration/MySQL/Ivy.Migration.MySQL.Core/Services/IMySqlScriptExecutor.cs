using MySql.Data.MySqlClient;

namespace Ivy.Migration.MySQL.Core.Services
{
    public interface IMySqlScriptExecutor
    {
        void Execute(MySqlScript script);
    }
}
