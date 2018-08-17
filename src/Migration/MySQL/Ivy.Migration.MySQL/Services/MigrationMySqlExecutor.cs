using Ivy.Data.Core.Interfaces;
using Ivy.Migration.Core.Interfaces.Services;
using Ivy.Migration.MySQL.Core.Services;

namespace Ivy.Migration.MySQL.Services
{
    public class MigrationMySqlExecutor : IMigrationSqlExecutor
    {
        #region Variables & Constants

        private readonly ITransactionHelper _tranHelper;

        private readonly IMySqlScriptBuilder _scriptBuilder;
        private readonly IMySqlScriptExecutor _scriptExecutor;

        #endregion

        #region Constructor

        public MigrationMySqlExecutor(
            ITransactionHelper tranHelper,
            IMySqlScriptBuilder scriptBuilder,
            IMySqlScriptExecutor scriptExecutor)
        {
            _tranHelper = tranHelper;
            _scriptBuilder = scriptBuilder;
            _scriptExecutor = scriptExecutor;
        }

        #endregion

        #region Public Methods

        public void ExecuteSql(string scriptText, string connectionString)
        {
            _tranHelper.WrapInTransaction(tran => {

                var script = _scriptBuilder.GenerateScript(tran, scriptText);

                _scriptExecutor.Execute(script);
            }, connectionString);
        }

        #endregion
    }
}
