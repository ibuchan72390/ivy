using Ivy.Data.Core.Interfaces;
using Ivy.Migration.Core.Interfaces.Services;
using Ivy.Migration.MySQL.Core.Services;

namespace Ivy.Migration.MySQL.Services
{
    public class MigrationMySqlExecutor : IMigrationSqlExecutor
    {
        #region Variables & Constants

        private readonly ITranConnGenerator _tcGen;

        private readonly IMySqlScriptBuilder _scriptBuilder;
        private readonly IMySqlScriptExecutor _scriptExecutor;

        #endregion

        #region Constructor

        public MigrationMySqlExecutor(
            ITranConnGenerator tcGen,
            IMySqlScriptBuilder scriptBuilder,
            IMySqlScriptExecutor scriptExecutor)
        {
            _tcGen = tcGen;
            _scriptBuilder = scriptBuilder;
            _scriptExecutor = scriptExecutor;
        }

        #endregion

        #region Public Methods

        public void ExecuteSql(string scriptText, string connectionString)
        {
            using (ITranConn tc = _tcGen.GenerateTranConn(connectionString))
            {
                var script = _scriptBuilder.GenerateScript(tc, scriptText);

                _scriptExecutor.Execute(script);

                tc.Transaction.Commit();
                tc.Connection.Close();
            }
        }

        #endregion
    }
}
