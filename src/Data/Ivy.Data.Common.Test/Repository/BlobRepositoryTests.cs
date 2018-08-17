using Ivy.Data.Core.Interfaces;
using Ivy.TestHelper.TestEntities;
using Moq;
using System.Linq;
using Xunit;

/*
 * Through the BlobRepository, we will also test our BaseRepository
 */
namespace Ivy.Data.Common.Test.Repository
{
    public class BlobRepositoryTests :
        RepositoryTestBase<IBlobRepository<TestEnumEntity>>
    {
        #region Tests

        #region Insert

        [Fact]
        public void Insert_Executes_As_Expected()
        {
            var entity = new TestEnumEntity();

            _mockSqlGen.Setup(x => x.GenerateInsertQuery(entity, parms)).Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteNonQuery(sql, ConnectionString, tc.Object, execResult.Parms));

            Sut.Insert(entity, tc.Object);

            _mockSqlGen.Verify(x => x.GenerateInsertQuery(entity, parms), Times.Once);

            _mockSqlExec.Verify(x => x.ExecuteNonQuery(sql, ConnectionString, tc.Object, execResult.Parms), Times.Once);
        }

        #endregion

        #region BulkInsert

        [Fact]
        public void BulkInsert_Executes_As_Expected_With_TranConn()
        {
            var entities = Enumerable.Range(0, 4).Select(x => new TestEnumEntity()).ToList();

            _mockSqlGen.Setup(x => x.GenerateInsertQuery(entities, parms)).Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteNonQuery(sql, ConnectionString, tc.Object, execResult.Parms));

            Sut.BulkInsert(entities, tc.Object);

            _mockSqlGen.Verify(x => x.GenerateInsertQuery(entities, parms), Times.Once);

            _mockSqlExec.Verify(x => x.ExecuteNonQuery(sql, ConnectionString, tc.Object, execResult.Parms), Times.Once);
        }

        #endregion

        #endregion
    }
}
