using Ivy.Data.Core.Domain;
using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.Init;
using Ivy.Data.Core.Interfaces.SQL;
using Ivy.IoC.Core;
using Ivy.TestHelper.TestEntities;
using Moq;
using System.Collections.Generic;

namespace Ivy.Data.Common.Test.Repository
{
    public abstract class RepositoryTestBase<T> :
        CommonDataTestBase<T>
        where T : class, IInitialize
    {
        #region Variables & Constants

        protected Mock<IDatabaseKeyManager> _mockKeyMgr;
        protected Mock<ITransactionHelper> _mockTranHelper;
        protected Mock<ISqlGenerator<TestEnumEntity>> _mockSqlGen;
        protected Mock<ISqlGenerator<TestEnumEntity, int>> _mockEntitySqlGen;
        protected Mock<ISqlExecutor> _mockSqlExec;

        protected const string sql = "SQL";
        protected const string ConnectionString = "ConnectionString";

        protected readonly Mock<ITranConn> tc = new Mock<ITranConn>();

        protected static readonly Dictionary<string, object> parms = new Dictionary<string, object>();
        protected readonly SqlExecutionResult execResult = new SqlExecutionResult(sql, parms);


        #endregion

        #region SetUp & TearDown

        public RepositoryTestBase()
        {
            Sut.InitializeByConnectionString(ConnectionString);
        }

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockKeyMgr = InitializeMoq<IDatabaseKeyManager>(containerGen);
            _mockTranHelper = InitializeMoq<ITransactionHelper>(containerGen);
            _mockSqlGen = InitializeMoq<ISqlGenerator<TestEnumEntity>>(containerGen);
            _mockEntitySqlGen = InitializeMoq<ISqlGenerator<TestEnumEntity, int>>(containerGen);
            _mockSqlExec = InitializeMoq<ISqlExecutor>(containerGen);
        }

        #endregion
    }
}
