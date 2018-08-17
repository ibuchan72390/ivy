using Ivy.Data.Common.Transaction;
using Ivy.Data.Core.Interfaces;
using Ivy.IoC.Core;
using Ivy.TestHelper.TestEntities;
using Moq;
using System;
using System.Data;
using System.Threading.Tasks;
using Xunit;

namespace Ivy.Data.Common.Test
{
    public class TransactionHelperTests :
        CommonDataTestBase<ITransactionHelper>
    {
        #region Variables & Constants

        private static readonly ParentEntity entity = new ParentEntity();
        private static int actionHits = 0;
        private static int funcHits = 0;

        private Mock<ITranConnGenerator> _mockTranGen;
        private ITranConn _mockTc;
        private Mock<IDbConnection> _mockConn;
        private Mock<IDbTransaction> _mockTran;

        private const string ConnectionString = "connection_string";

        private readonly Action<ITranConn> tcAction = (ITranConn tc) => {
            actionHits++;
        };

        private readonly Func<ITranConn, Task> tcAsyncAction = async (ITranConn tc) => {
            await Task.FromResult(0);
            actionHits++;
        };

        private readonly Func<ITranConn, ParentEntity> tcFunc = (ITranConn tc) => {
            funcHits++;
            return entity;
        };

        private readonly Func<ITranConn, Task<ParentEntity>> tcAsyncFunc = async (ITranConn tc) => {
            await Task.FromResult(0);
            funcHits++;
            return entity;
        };

        private readonly Action<ITranConn> tcActionErr = (ITranConn tc) => {
            actionHits++;
            throw new Exception();
        };

        private readonly Func<ITranConn, Task> tcAsyncActionErr = async (ITranConn tc) => {
            await Task.FromResult(0);
            actionHits++;
            throw new Exception();
        };

        private readonly Func<ITranConn, ParentEntity> tcFuncErr = (ITranConn tc) => {
            funcHits++;
            throw new Exception();
        };

        private readonly Func<ITranConn, Task<ParentEntity>> tcAsyncFuncErr = async (ITranConn tc) => {
            await Task.FromResult(0);
            funcHits++;
            throw new Exception();
        };

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockTranGen = InitializeMoq<ITranConnGenerator>(containerGen);

            _mockConn = new Mock<IDbConnection>();
            _mockTran = new Mock<IDbTransaction>();

            _mockTc = new TranConn(_mockConn.Object, _mockTran.Object);

            _mockTranGen.Setup(x => x.GenerateTranConn(ConnectionString, IsolationLevel.ReadUncommitted)).
                Returns(_mockTc);

            actionHits = 0;
            funcHits = 0;
        }

        #endregion

        #region Tests

        [Fact]
        public void WrapInTransaction_Executes_As_Expected_With_TranConn()
        {
            Sut.WrapInTransaction(tcAction, ConnectionString, _mockTc);

            Assert.Equal(1, actionHits);

            _mockTran.Verify(x => x.Commit(), Times.Never);
            _mockTran.Verify(x => x.Dispose(), Times.Never);
        }

        [Fact]
        public void WrapInTransaction_Execute_Errors_As_Expected_With_TranConn()
        {
            Assert.Throws<Exception>(() => Sut.WrapInTransaction(tcActionErr, ConnectionString, _mockTc));

            Assert.Equal(1, actionHits);

            _mockTran.Verify(x => x.Commit(), Times.Never);
            _mockTran.Verify(x => x.Rollback(), Times.Never);
            _mockTran.Verify(x => x.Dispose(), Times.Never);
        }

        [Fact]
        public void WrapInTransaction_Returns_As_Expected_With_TranConn()
        {
            var result = Sut.WrapInTransaction<ParentEntity>(tcFunc, ConnectionString, _mockTc);

            Assert.Same(entity, result);

            _mockTran.Verify(x => x.Commit(), Times.Never);
            _mockTran.Verify(x => x.Dispose(), Times.Never);
        }

        [Fact]
        public void WrapInTransaction_Return_Errors_As_Expected_With_TranConn()
        {
            Assert.Throws<Exception>(() => Sut.WrapInTransaction<ParentEntity>(tcFuncErr, ConnectionString, _mockTc));

            _mockTran.Verify(x => x.Commit(), Times.Never);
            _mockTran.Verify(x => x.Rollback(), Times.Never);
            _mockTran.Verify(x => x.Dispose(), Times.Never);
        }

        [Fact]
        public void WrapInTransaction_Executes_As_Expected_With_ConnectionString()
        {
            Sut.WrapInTransaction(tcAction, ConnectionString);

            Assert.Equal(1, actionHits);

            _mockTran.Verify(x => x.Commit(), Times.Once);
            _mockTran.Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public void WrapInTransaction_Execute_Errors_As_Expected_With_ConnectionString()
        {
            Assert.Throws<Exception>(() => Sut.WrapInTransaction(tcActionErr, ConnectionString));

            Assert.Equal(1, actionHits);

            _mockTran.Verify(x => x.Commit(), Times.Never);
            _mockTran.Verify(x => x.Rollback(), Times.Once);
            _mockTran.Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public void WrapInTransaction_Returns_As_Expected_With_ConnectionString()
        {
            var result = Sut.WrapInTransaction<ParentEntity>(tcFunc, ConnectionString);

            Assert.Same(entity, result);

            _mockTran.Verify(x => x.Commit(), Times.Once);
            _mockTran.Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public void WrapInTransaction_Return_Errors_As_Expected_With_ConnectionString()
        {
            Assert.Throws<Exception>(() => Sut.WrapInTransaction<ParentEntity>(tcFuncErr, ConnectionString));

            _mockTran.Verify(x => x.Commit(), Times.Never);
            _mockTran.Verify(x => x.Rollback(), Times.Once);
            _mockTran.Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public async void WrapInTransactionAsync_Executes_As_Expected_With_TranConn()
        {
            await Sut.WrapInTransactionAsync(tcAsyncAction, ConnectionString, _mockTc);

            Assert.Equal(1, actionHits);

            _mockTran.Verify(x => x.Commit(), Times.Never);
            _mockTran.Verify(x => x.Dispose(), Times.Never);
        }

        [Fact]
        public async void WrapInTransactionAsync_Execute_Errors_As_Expected_With_TranConn()
        {
            await Assert.ThrowsAsync<Exception>(() => Sut.WrapInTransactionAsync(tcAsyncActionErr, ConnectionString, _mockTc));

            Assert.Equal(1, actionHits);

            _mockTran.Verify(x => x.Commit(), Times.Never);
            _mockTran.Verify(x => x.Rollback(), Times.Never);
            _mockTran.Verify(x => x.Dispose(), Times.Never);
        }

        [Fact]
        public async void WrapInTransactionAsync_Returns_As_Expected_With_TranConn()
        {
            var result = await Sut.WrapInTransactionAsync<ParentEntity>(tcAsyncFunc, ConnectionString, _mockTc);

            Assert.Same(entity, result);

            _mockTran.Verify(x => x.Commit(), Times.Never);
            _mockTran.Verify(x => x.Dispose(), Times.Never);
        }

        [Fact]
        public async void WrapInTransactionAsync_Return_Errors_As_Expected_With_TranConn()
        {
            await Assert.ThrowsAsync<Exception>(async () => await Sut.WrapInTransactionAsync<ParentEntity>(tcAsyncFuncErr, ConnectionString, _mockTc));

            _mockTran.Verify(x => x.Commit(), Times.Never);
            _mockTran.Verify(x => x.Rollback(), Times.Never);
            _mockTran.Verify(x => x.Dispose(), Times.Never);
        }

        [Fact]
        public async void WrapInTransactionAsync_Executes_As_Expected_With_ConnectionString()
        {
            await Sut.WrapInTransactionAsync(tcAsyncAction, ConnectionString);

            Assert.Equal(1, actionHits);

            _mockTran.Verify(x => x.Commit(), Times.Once);
            _mockTran.Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public async void WrapInTransactionAsync_Execute_Errors_As_Expected_With_ConnectionString()
        {
            await Assert.ThrowsAsync<Exception>(() => Sut.WrapInTransactionAsync(tcAsyncActionErr, ConnectionString));

            Assert.Equal(1, actionHits);

            _mockTran.Verify(x => x.Commit(), Times.Never);
            _mockTran.Verify(x => x.Rollback(), Times.Once);
            _mockTran.Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public async void WrapInTransactionAsync_Returns_As_Expected_With_ConnectionString()
        {
            var result = await Sut.WrapInTransactionAsync<ParentEntity>(tcAsyncFunc, ConnectionString);

            Assert.Same(entity, result);

            _mockTran.Verify(x => x.Commit(), Times.Once);
            _mockTran.Verify(x => x.Dispose(), Times.Once);
        }

        [Fact]
        public async void WrapInTransactionAsync_Return_Errors_As_Expected_With_ConnectionString()
        {
            await Assert.ThrowsAsync<Exception>(() => Sut.WrapInTransactionAsync<ParentEntity>(tcAsyncFuncErr, ConnectionString));

            _mockTran.Verify(x => x.Commit(), Times.Never);
            _mockTran.Verify(x => x.Rollback(), Times.Once);
            _mockTran.Verify(x => x.Dispose(), Times.Once);
        }

        #endregion
    }
}
