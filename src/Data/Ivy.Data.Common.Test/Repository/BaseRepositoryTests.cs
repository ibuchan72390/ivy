using Ivy.Data.Common.Pagination;
using Ivy.Data.Core.Interfaces;
using Ivy.TestHelper.TestEntities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ivy.Data.Common.Test.Repository
{
    public class BaseRepositoryTests : 
        RepositoryTestBase<IBlobRepository<TestEnumEntity>>
    {
        #region Tests

        #region DeleteAll

        [Fact]
        public void DeleteAll_Executes_As_Expected()
        {
            // Arrange
            _mockSqlGen.Setup(x => x.GenerateDeleteQuery(null)).Returns(sql);

            _mockSqlExec.Setup(x => x.ExecuteNonQuery(sql, ConnectionString, tc.Object, It.IsAny<object>()));

            // Act
            Sut.DeleteAll(tc.Object);


            // Assert
            _mockSqlGen.Verify(x => x.GenerateDeleteQuery(null), Times.Once);

            _mockSqlExec.Verify(x => x.ExecuteNonQuery(sql, ConnectionString, tc.Object, It.IsAny<object>()), Times.Once);
        }

        #endregion

        #region DeleteAllAsync

        [Fact]
        public async void DeleteAllAsync_Executes_As_Expected()
        {
            // Arrange
            _mockSqlGen.Setup(x => x.GenerateDeleteQuery(null)).Returns(sql);

            _mockSqlExec.Setup(x => x.ExecuteNonQueryAsync(sql, ConnectionString, tc.Object, It.IsAny<object>())).Returns(Task.FromResult(0));

            // Act
            await Sut.DeleteAllAsync(tc.Object);


            // Assert
            _mockSqlGen.Verify(x => x.GenerateDeleteQuery(null), Times.Once);

            _mockSqlExec.Verify(x => x.ExecuteNonQueryAsync(sql, ConnectionString, tc.Object, It.IsAny<object>()), Times.Once);
        }

        #endregion

        #region GetAll

        [Fact]
        public void GetAll_Executes_As_Expected()
        {
            // Arrange
            var expected = new List<TestEnumEntity>();

            _mockSqlGen.Setup(x => x.GenerateGetQuery(null, null, null, null, null, null)).Returns(sql);

            _mockTranHelper.Setup(x => x.WrapInTransaction(It.IsAny<Func<ITranConn, IEnumerable<TestEnumEntity>>>(), ConnectionString, tc.Object)).Returns(expected);

            // Act
            var results = Sut.GetAll(tc.Object);

            // Assert
            Assert.Same(expected, results);

            _mockSqlGen.Verify(x => x.GenerateGetQuery(null, null, null, null, null, null), Times.Once);

            _mockTranHelper.Verify(x => x.WrapInTransaction(It.IsAny<Func<ITranConn, IEnumerable<TestEnumEntity>>>(), ConnectionString, tc.Object), Times.Once);
        }

        #endregion

        #region GetAllAsync

        [Fact]
        public async void GetAllAsync_Executes_As_Expected()
        {
            // Arrange
            var expected = new List<TestEnumEntity>();

            _mockSqlGen.Setup(x => x.GenerateGetQuery(null, null, null, null, null, null)).Returns(sql);

            _mockTranHelper.Setup(x => x.WrapInTransactionAsync(It.IsAny<Func<ITranConn, Task<IEnumerable<TestEnumEntity>>>>(), ConnectionString, tc.Object)).ReturnsAsync(expected);

            // Act
            var results = await Sut.GetAllAsync(tc.Object);

            // Assert
            Assert.Same(expected, results);

            _mockSqlGen.Verify(x => x.GenerateGetQuery(null, null, null, null, null, null), Times.Once);

            _mockTranHelper.Verify(x => x.WrapInTransactionAsync(It.IsAny<Func<ITranConn, Task<IEnumerable<TestEnumEntity>>>>(), ConnectionString, tc.Object), Times.Once);
        }

        #endregion

        #region GetAll (Paginated)

        [Fact]
        public void GetAllPaginated_Executes_As_Expected()
        {
            var req = new PaginationRequest();
            const int count = 5;

            var data = Enumerable.Range(0, count).Select(x => new TestEnumEntity()).ToList();

            var offset = (req.PageNumber - 1) * req.PageCount;

            _mockSqlGen.
                Setup(x => x.GenerateGetQuery(null, null, null, null, req.PageCount, offset)).
                Returns(sql);

            _mockTranHelper.
                Setup(x => x.WrapInTransaction<IEnumerable<TestEnumEntity>>(It.IsAny<Func<ITranConn, IEnumerable<TestEnumEntity>>>(), ConnectionString, tc.Object)).
                Returns(data);

            _mockSqlGen.
                Setup(x => x.GenerateGetCountQuery(null, null)).
                Returns(sql);

            _mockSqlExec.
                Setup(x => x.ExecuteTypedQuery<int>(sql, ConnectionString, tc.Object, null)).
                Returns(new List<int> { count });

            var result = Sut.GetAll(req, tc.Object);

            Assert.Equal(result.TotalCount, count);
            Assert.Same(result.Data, data);

            _mockSqlGen.
                Verify(x => x.GenerateGetQuery(null, null, null, null, req.PageCount, offset), Times.Once);

            _mockTranHelper.
                Verify(x => x.WrapInTransaction<IEnumerable<TestEnumEntity>>(It.IsAny<Func<ITranConn, IEnumerable<TestEnumEntity>>>(), ConnectionString, tc.Object), Times.Once);

            _mockSqlGen.
                Verify(x => x.GenerateGetCountQuery(null, null), Times.Once);

            _mockSqlExec.
                Verify(x => x.ExecuteTypedQuery<int>(sql, ConnectionString, tc.Object, null), Times.Once);
        }

        #endregion

        #region GetAllAsync (Paginated)

        [Fact]
        public async void GetAllAsyncPaginated_Executes_As_Expected()
        {
            var req = new PaginationRequest();
            const int count = 5;

            var data = Enumerable.Range(0, count).Select(x => new TestEnumEntity()).ToList();

            var offset = (req.PageNumber - 1) * req.PageCount;

            _mockSqlGen.
                Setup(x => x.GenerateGetQuery(null, null, null, null, req.PageCount, offset)).
                Returns(sql);

            _mockTranHelper.
                Setup(x => x.WrapInTransactionAsync<IEnumerable<TestEnumEntity>>(It.IsAny<Func<ITranConn, Task<IEnumerable<TestEnumEntity>>>>(), ConnectionString, tc.Object)).
                ReturnsAsync(data);

            _mockSqlGen.
                Setup(x => x.GenerateGetCountQuery(null, null)).
                Returns(sql);

            _mockSqlExec.
                Setup(x => x.ExecuteTypedQueryAsync<int>(sql, ConnectionString, tc.Object, null)).
                ReturnsAsync(new List<int> { count });

            var result = await Sut.GetAllAsync(req, tc.Object);

            Assert.Equal(result.TotalCount, count);
            Assert.Same(result.Data, data);

            _mockSqlGen.
                Verify(x => x.GenerateGetQuery(null, null, null, null, req.PageCount, offset), Times.Once);

            _mockTranHelper.
                Verify(x => x.WrapInTransactionAsync<IEnumerable<TestEnumEntity>>(It.IsAny<Func<ITranConn, Task<IEnumerable<TestEnumEntity>>>>(), ConnectionString, tc.Object), Times.Once);

            _mockSqlGen.
                Verify(x => x.GenerateGetCountQuery(null, null), Times.Once);

            _mockSqlExec.
                Verify(x => x.ExecuteTypedQueryAsync<int>(sql, ConnectionString, tc.Object, null), Times.Once);
        }

        #endregion

        #region GetCount

        [Fact]
        public void GetCount_Executes_As_Expected()
        {
            const int count = 123;

            _mockSqlGen.Setup(x => x.GenerateGetCountQuery(null, null)).Returns(sql);

            _mockSqlExec.Setup(x => x.ExecuteTypedQuery<int>(sql, ConnectionString, tc.Object, null)).Returns(new List<int> { count });

            var result = Sut.GetCount(tc.Object);

            Assert.Equal(count, result);

            _mockSqlGen.Verify(x => x.GenerateGetCountQuery(null, null), Times.Once);

            _mockSqlExec.Verify(x => x.ExecuteTypedQuery<int>(sql, ConnectionString, tc.Object, null), Times.Once);
        }

        #endregion

        #region GetCountAsync

        [Fact]
        public async void GetCountAsync_Executes_As_Expected()
        {
            const int count = 123;

            _mockSqlGen.Setup(x => x.GenerateGetCountQuery(null, null)).Returns(sql);

            _mockSqlExec.Setup(x => x.ExecuteTypedQueryAsync<int>(sql, ConnectionString, tc.Object, null)).ReturnsAsync(new List<int> { count });

            var result = await Sut.GetCountAsync(tc.Object);

            Assert.Equal(count, result);

            _mockSqlGen.Verify(x => x.GenerateGetCountQuery(null, null), Times.Once);

            _mockSqlExec.Verify(x => x.ExecuteTypedQueryAsync<int>(sql, ConnectionString, tc.Object, null), Times.Once);
        }

        #endregion

        #endregion
    }
}
