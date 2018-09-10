using Ivy.Migration.Core.Interfaces.Services;
using Ivy.Migration.MySQL.Core.Services;
using MySql.Data.MySqlClient;

namespace Ivy.Migration.MySQL.Services
{
    public class MigrationMySqlExecutor : IMigrationSqlExecutor
    {
        #region Variables & Constants

        private readonly IMySqlScriptBuilder _scriptBuilder;
        private readonly IMySqlScriptExecutor _scriptExecutor;

        #endregion

        #region Constructor

        public MigrationMySqlExecutor(
            IMySqlScriptBuilder scriptBuilder,
            IMySqlScriptExecutor scriptExecutor)
        {
            _scriptBuilder = scriptBuilder;
            _scriptExecutor = scriptExecutor;
        }

        #endregion

        #region Public Methods

        public void ExecuteSql(string scriptText, string connectionString)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                var script = _scriptBuilder.GenerateScript(conn, scriptText);

                _scriptExecutor.Execute(script);

                conn.Close();
            }
        }

        #endregion
    }
}
