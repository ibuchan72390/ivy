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
    public class EntityRepositoryTests :
        RepositoryTestBase<IEntityRepository<TestEnumEntity>>
    {
        #region Variables & Constants

        private const int entityId = 123;

        private readonly TestEnumEntity entity = new TestEnumEntity { Id = entityId };

        private static readonly IEnumerable<int> entityIds = Enumerable.Range(0, 3);

        private readonly IEnumerable<TestEnumEntity> entities = entityIds.
            Select(x => new TestEnumEntity { Id = x }).ToList();

        #endregion

        #region Tests

        #region Delete

        [Fact]
        public void Delete_Executes_As_Expected()
        {
            _mockEntitySqlGen.
                Setup(x => x.GenerateDeleteQuery(entity.Id, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteNonQuery(execResult.Sql, ConnectionString, tc.Object, execResult.Parms));

            Sut.Delete(entity, tc.Object);

            _mockEntitySqlGen.
                Setup(x => x.GenerateDeleteQuery(entity.Id, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteNonQuery(execResult.Sql, ConnectionString, tc.Object, execResult.Parms));
        }

        #endregion

        #region DeleteAsync

        [Fact]
        public async void DeleteAsync_Executes_As_Expected()
        {
            _mockEntitySqlGen.
                Setup(x => x.GenerateDeleteQuery(entity.Id, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteNonQueryAsync(execResult.Sql, ConnectionString, tc.Object, execResult.Parms)).Returns(Task.FromResult(0));

            await Sut.DeleteAsync(entity, tc.Object);

            _mockEntitySqlGen.
                Setup(x => x.GenerateDeleteQuery(entity.Id, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteNonQueryAsync(execResult.Sql, ConnectionString, tc.Object, execResult.Parms));
        }

        #endregion

        #region DeleteById

        [Fact]
        public void DeleteById_Executes_As_Expected()
        {
            _mockEntitySqlGen.
                Setup(x => x.GenerateDeleteQuery(entity.Id, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteNonQuery(execResult.Sql, ConnectionString, tc.Object, execResult.Parms));

            Sut.DeleteById(entity.Id, tc.Object);

            _mockEntitySqlGen.
                Setup(x => x.GenerateDeleteQuery(entity.Id, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteNonQuery(execResult.Sql, ConnectionString, tc.Object, execResult.Parms));
        }

        #endregion

        #region DeleteByIdAsync

        [Fact]
        public async void DeleteByIdAsync_Executes_As_Expected()
        {
            _mockEntitySqlGen.
                Setup(x => x.GenerateDeleteQuery(entity.Id, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteNonQueryAsync(execResult.Sql, ConnectionString, tc.Object, execResult.Parms)).Returns(Task.FromResult(0));

            await Sut.DeleteByIdAsync(entity.Id, tc.Object);

            _mockEntitySqlGen.
                Setup(x => x.GenerateDeleteQuery(entity.Id, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteNonQueryAsync(execResult.Sql, ConnectionString, tc.Object, execResult.Parms));
        }

        #endregion

        #region Delete (Many)

        [Fact]
        public void Delete_Many_Executes_As_Expected_For_Null_Entities()
        {
            IEnumerable<TestEnumEntity> entities = null;

            Sut.Delete(entities);
        }

        [Fact]
        public void Delete_Many_Executes_As_Expected_For_Empty_Entities()
        {
            IEnumerable<TestEnumEntity> entities = new List<TestEnumEntity>();

            Sut.Delete(entities);
        }

        [Fact]
        public void Delete_Many_Executes_As_Expected()
        {
            _mockEntitySqlGen.
                Setup(x => x.GenerateDeleteQuery(entityIds, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteNonQuery(execResult.Sql, ConnectionString, tc.Object, execResult.Parms));

            Sut.Delete(entities, tc.Object);

            _mockEntitySqlGen.Verify(x => x.GenerateDeleteQuery(entityIds, It.IsAny<Dictionary<string, object>>()), Times.Once);

            _mockSqlExec.Verify(x => x.ExecuteNonQuery(execResult.Sql, ConnectionString, tc.Object, execResult.Parms), Times.Once);
        }

        #endregion

        #region DeleteAsync (Many)

        [Fact]
        public async void DeleteAsync_Many_Executes_As_Expected_For_Null_Entities()
        {
            IEnumerable<TestEnumEntity> entities = null;

            await Sut.DeleteAsync(entities);
        }

        [Fact]
        public async void DeleteAsync_Many_Executes_As_Expected_For_Empty_Entities()
        {
            IEnumerable<TestEnumEntity> entities = new List<TestEnumEntity>();

            await Sut.DeleteAsync(entities);
        }

        [Fact]
        public async void DeleteAsync_Many_Executes_As_Expected()
        {
            _mockEntitySqlGen.
                Setup(x => x.GenerateDeleteQuery(entityIds, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteNonQueryAsync(execResult.Sql, ConnectionString, tc.Object, execResult.Parms)).Returns(Task.FromResult(0));

            await Sut.DeleteAsync(entities, tc.Object);

            _mockEntitySqlGen.Verify(x => x.GenerateDeleteQuery(entityIds, It.IsAny<Dictionary<string, object>>()), Times.Once);

            _mockSqlExec.Verify(x => x.ExecuteNonQueryAsync(execResult.Sql, ConnectionString, tc.Object, execResult.Parms), Times.Once);
        }

        #endregion

        #region DeleteByIdList

        [Fact]
        public void DeleteByIdList_Executes_As_Expected_For_Null_Entities()
        {
            IEnumerable<int> entities = null;

            Sut.DeleteByIdList(entities);
        }

        [Fact]
        public void DeleteByIdList_Executes_As_Expected_For_Empty_Entities()
        {
            IEnumerable<int> entities = new List<int>();

            Sut.DeleteByIdList(entities);
        }

        [Fact]
        public void DeleteByIdList_Executes_As_Expected()
        {
            _mockEntitySqlGen.
                Setup(x => x.GenerateDeleteQuery(entityIds, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteNonQuery(execResult.Sql, ConnectionString, tc.Object, execResult.Parms));

            Sut.DeleteByIdList(entityIds, tc.Object);

            _mockEntitySqlGen.Verify(x => x.GenerateDeleteQuery(entityIds, It.IsAny<Dictionary<string, object>>()), Times.Once);

            _mockSqlExec.Verify(x => x.ExecuteNonQuery(execResult.Sql, ConnectionString, tc.Object, execResult.Parms), Times.Once);
        }

        #endregion

        #region DeleteByIdListAsync

        [Fact]
        public async void DeleteByIdListAsync_Executes_As_Expected_For_Null_Entities()
        {
            IEnumerable<int> entities = null;

            await Sut.DeleteByIdListAsync(entities);
        }

        [Fact]
        public async void DeleteByIdListAsync_Executes_As_Expected_For_Empty_Entities()
        {
            IEnumerable<int> entities = new List<int>();

            await Sut.DeleteByIdListAsync(entities);
        }

        [Fact]
        public async void DeleteByIdListAsync_Executes_As_Expected()
        {
            _mockEntitySqlGen.
                Setup(x => x.GenerateDeleteQuery(entityIds, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteNonQueryAsync(execResult.Sql, ConnectionString, tc.Object, execResult.Parms)).Returns(Task.FromResult(0));

            await Sut.DeleteByIdListAsync(entityIds, tc.Object);

            _mockEntitySqlGen.Verify(x => x.GenerateDeleteQuery(entityIds, It.IsAny<Dictionary<string, object>>()), Times.Once);

            _mockSqlExec.Verify(x => x.ExecuteNonQueryAsync(execResult.Sql, ConnectionString, tc.Object, execResult.Parms), Times.Once);
        }

        #endregion

        #region GetById

        [Fact]
        public void GetById_Executes_As_Expected()
        {
            var results = new List<TestEnumEntity> { entity };

            _mockEntitySqlGen.
                Setup(x => x.GenerateGetQuery(entity.Id, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockTranHelper.
                Setup(x => x.WrapInTransaction(It.IsAny<Func<ITranConn, IEnumerable<TestEnumEntity>>>(), ConnectionString, tc.Object)).
                Returns(results);

            var result = Sut.GetById(entity.Id, tc.Object);

            Assert.Same(result, entity);

            _mockEntitySqlGen.
                Verify(x => x.GenerateGetQuery(entity.Id, It.IsAny<Dictionary<string, object>>()), Times.Once);

            _mockTranHelper.
                Verify(x => x.WrapInTransaction(It.IsAny<Func<ITranConn, IEnumerable<TestEnumEntity>>>(), ConnectionString, tc.Object), Times.Once);
        }

        #endregion

        #region GetByIdAsync

        [Fact]
        public async void GetByIdAsync_Executes_As_Expected()
        {
            var results = new List<TestEnumEntity> { entity };

            _mockEntitySqlGen.
                Setup(x => x.GenerateGetQuery(entity.Id, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockTranHelper.
                Setup(x => x.WrapInTransactionAsync(It.IsAny<Func<ITranConn, Task<IEnumerable<TestEnumEntity>>>>(), ConnectionString, tc.Object)).
                ReturnsAsync(results);

            var result = await Sut.GetByIdAsync(entity.Id, tc.Object);

            Assert.Same(result, entity);

            _mockEntitySqlGen.
                Verify(x => x.GenerateGetQuery(entity.Id, It.IsAny<Dictionary<string, object>>()), Times.Once);

            _mockTranHelper.
                Verify(x => x.WrapInTransactionAsync(It.IsAny<Func<ITranConn, Task<IEnumerable<TestEnumEntity>>>>(), ConnectionString, tc.Object), Times.Once);
        }

        #endregion

        #region GetByIdList

        [Fact]
        public void GetByIdList_Executes_As_Expected()
        {
            _mockEntitySqlGen.
                Setup(x => x.GenerateGetQuery(entityIds, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockTranHelper.
                Setup(x => x.WrapInTransaction(It.IsAny<Func<ITranConn, IEnumerable<TestEnumEntity>>>(), ConnectionString, tc.Object)).
                Returns(entities);

            var result = Sut.GetByIdList(entityIds, tc.Object);

            Assert.Same(result, entities);

            _mockEntitySqlGen.
                Verify(x => x.GenerateGetQuery(entityIds, It.IsAny<Dictionary<string, object>>()), Times.Once);

            _mockTranHelper.
                Verify(x => x.WrapInTransaction(It.IsAny<Func<ITranConn, IEnumerable<TestEnumEntity>>>(), ConnectionString, tc.Object), Times.Once);
        }

        #endregion

        #region GetByIdListAsync

        [Fact]
        public async void GetByIdListAsync_Executes_As_Expected()
        {
            _mockEntitySqlGen.
                Setup(x => x.GenerateGetQuery(entityIds, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockTranHelper.
                Setup(x => x.WrapInTransactionAsync(It.IsAny<Func<ITranConn, Task<IEnumerable<TestEnumEntity>>>>(), ConnectionString, tc.Object)).
                ReturnsAsync(entities);

            var result = await Sut.GetByIdListAsync(entityIds, tc.Object);

            Assert.Same(result, entities);

            _mockEntitySqlGen.
                Verify(x => x.GenerateGetQuery(entityIds, It.IsAny<Dictionary<string, object>>()), Times.Once);

            _mockTranHelper.
                Verify(x => x.WrapInTransactionAsync(It.IsAny<Func<ITranConn, Task<IEnumerable<TestEnumEntity>>>>(), ConnectionString, tc.Object), Times.Once);
        }

        #endregion

        #region SaveOrUpdate

        [Fact]
        public void SaveOrUpdate_Executes_As_Expected_And_New_Id()
        {
            const int newId = 987;

            _mockEntitySqlGen.
                Setup(x => x.GenerateSaveOrUpdateQuery(entity, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteTypedQuery<int>(execResult.Sql, ConnectionString, tc.Object, execResult.Parms)).
                Returns(new List<int> { newId });

            Sut.SaveOrUpdate(entity, tc.Object);

            Assert.Equal(newId, entity.Id);

            _mockEntitySqlGen.
                Verify(x => x.GenerateSaveOrUpdateQuery(entity, It.IsAny<Dictionary<string, object>>()), Times.Once);

            _mockSqlExec.Verify(x => x.ExecuteTypedQuery<int>(execResult.Sql, ConnectionString, tc.Object, execResult.Parms), Times.Once);
        }

        [Fact]
        public void SaveOrUpdate_Executes_As_Expected_And_No_New_Id()
        {
            _mockEntitySqlGen.
                Setup(x => x.GenerateSaveOrUpdateQuery(entity, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteTypedQuery<int>(execResult.Sql, ConnectionString, tc.Object, execResult.Parms)).
                Returns(new List<int> { });

            Sut.SaveOrUpdate(entity, tc.Object);

            Assert.Equal(entityId, entity.Id);

            _mockEntitySqlGen.
                Verify(x => x.GenerateSaveOrUpdateQuery(entity, It.IsAny<Dictionary<string, object>>()), Times.Once);

            _mockSqlExec.Verify(x => x.ExecuteTypedQuery<int>(execResult.Sql, ConnectionString, tc.Object, execResult.Parms), Times.Once);
        }

        #endregion

        #region SaveOrUpdateAsync

        [Fact]
        public async void SaveOrUpdateAsync_Executes_As_Expected_And_New_Id()
        {
            const int newId = 987;

            _mockEntitySqlGen.
                Setup(x => x.GenerateSaveOrUpdateQuery(entity, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteTypedQueryAsync<int>(execResult.Sql, ConnectionString, tc.Object, execResult.Parms)).
                ReturnsAsync(new List<int> { newId });

            await Sut.SaveOrUpdateAsync(entity, tc.Object);

            Assert.Equal(newId, entity.Id);

            _mockEntitySqlGen.
                Verify(x => x.GenerateSaveOrUpdateQuery(entity, It.IsAny<Dictionary<string, object>>()), Times.Once);

            _mockSqlExec.Verify(x => x.ExecuteTypedQueryAsync<int>(execResult.Sql, ConnectionString, tc.Object, execResult.Parms), Times.Once);
        }

        [Fact]
        public async void SaveOrUpdateAsync_Executes_As_Expected_And_No_New_Id()
        {
            _mockEntitySqlGen.
                Setup(x => x.GenerateSaveOrUpdateQuery(entity, It.IsAny<Dictionary<string, object>>())).
                Returns(execResult);

            _mockSqlExec.Setup(x => x.ExecuteTypedQueryAsync<int>(execResult.Sql, ConnectionString, tc.Object, execResult.Parms)).
                ReturnsAsync(new List<int> { });

            await Sut.SaveOrUpdateAsync(entity, tc.Object);

            Assert.Equal(entityId, entity.Id);

            _mockEntitySqlGen.
                Verify(x => x.GenerateSaveOrUpdateQuery(entity, It.IsAny<Dictionary<string, object>>()), Times.Once);

            _mockSqlExec.Verify(x => x.ExecuteTypedQueryAsync<int>(execResult.Sql, ConnectionString, tc.Object, execResult.Parms), Times.Once);
        }

        #endregion

        #region SaveOrUpdate (Many)

        /*
         * This is a pretty hard mechanism to test because of the functional pass to TranHelper
         * We can't really test the functional implementation, so we're just fucked here
         */

        #endregion

        #endregion
    }
}
