using Ivy.Data.Core.Interfaces;
using Ivy.Migration.MySQL.Core.Providers;
using Ivy.Migration.MySQL.Core.Services;
using Ivy.Utility.Core.Extensions;
using MySql.Data.MySqlClient;

namespace Ivy.Migration.MySQL.Services
{
    public class MySqlScriptBuilder :
        IMySqlScriptBuilder
    {
        #region Variables & Constants

        private readonly IMySqlMigrationConfigurationProvider _config;

        #endregion

        #region Constructor

        public MySqlScriptBuilder(
            IMySqlMigrationConfigurationProvider config)
        {
            _config = config;
        }

        #endregion

        #region Public Methods

        public MySqlScript GenerateScript(MySqlConnection conn, string scriptText)
        {
            var script = new MySqlScript(conn, scriptText);

            if (!_config.Delimiter.IsNullOrEmpty())
            {
                script.Delimiter = _config.Delimiter;
            }

            return script;
        }

        #endregion
    }
}
