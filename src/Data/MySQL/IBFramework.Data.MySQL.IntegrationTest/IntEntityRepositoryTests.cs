using IBFramework.Data.Core.Interfaces;
using IBFramework.IoC;
using IBFramework.TestHelper;
using IBFramework.TestHelper.TestEntities;
using IBFramework.TestHelper.TestValues;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IBFramework.Data.MySQL.IntegrationTest
{
    public class IntEntityRepositoryTests : MySqlIntegrationTestBase, IDisposable
    {
        #region Variables & Constants

        private IEntityRepository<ParentEntity, int> _sut;

        #endregion

        #region Constructor

        public IntEntityRepositoryTests()
        {
            _sut = ServiceLocator.Instance.Resolve<IEntityRepository<ParentEntity, int>>();
            _sut.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);
        }

        public void Dispose()
        {
            TestCleaner.CleanDatabase();
        }

        #endregion

        #region GetById

        [Fact]
        public void GetById_Works_As_Expected()
        {
            var entity = new ParentEntity().SaveForTest();

            var result = _sut.GetById(entity.Id);

            Assert.True(entity.Equals(result));
        }

        [Fact]
        public void GetById_Returns_Null_If_Doesnt_Exist()
        {
            var result = _sut.GetById(int.MaxValue);

            Assert.Null(result);
        }

        [Fact]
        public void GetById_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.Resolve<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            _sut.GetById(0, tc);

            tc.Dispose();
        }
        
        #endregion

        #region GetByIdList

        [Fact]
        public void GetByIdList_Returns_As_Expected()
        {
            const int entitiesToMake = 4;

            var entities = Enumerable.Range(0, entitiesToMake).Select(x => new ParentEntity().SaveForTest()).ToList();

            var entityIds = entities.Select(x => x.Id);

            var results = _sut.GetByIdList(entityIds);

            foreach (var entity in entities)
            {
                var result = results.FirstOrDefault(x => x.Id == entity.Id);

                Assert.NotNull(result);
            }
        }

        [Fact]
        public void GetByIdList_Returns_Null_If_None_Exist()
        {
            const int idsToMake = 4;

            var ids = Enumerable.Range(-idsToMake, idsToMake);

            var results = _sut.GetByIdList(ids);

            Assert.Empty(results);
        }

        [Fact]
        public void GetByIdList_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.Resolve<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            _sut.GetByIdList(new List<int> { 0 }, tc);

            tc.Dispose();
        }

        [Fact]
        public void GetByIdList_Returns_Empty_On_Empty_List()
        {
            Assert.Empty(_sut.GetByIdList(new List<int>()));
        }

        [Fact]
        public void GetByIdList_Returns_Empty_On_Null_List()
        {
            Assert.Empty(_sut.GetByIdList(null));

        }

        #endregion

        #region SaveOrUpdate

        [Fact]
        public void SaveOrUpdate_Identifies_New_Save_Correctly()
        {
            var testEntity = new ParentEntity().GenerateForTest();

            var result = _sut.SaveOrUpdate(testEntity);

            Assert.True(result.Id > 0);

            var secondaryResult = _sut.GetById(result.Id);

            Assert.True(result.Equals(secondaryResult));
        }

        [Fact]
        public void SaveOrUpdate_Identifies_Existing_Update_Correctly()
        {
            var testEntity = new ParentEntity().SaveForTest();

            Assert.True(testEntity.Id > 0);

            testEntity.Integer = testEntity.Integer++;

            var result = _sut.SaveOrUpdate(testEntity);

            Assert.True(result.Id == testEntity.Id);
            Assert.Equal(result.Integer, testEntity.Integer);
            Assert.True(result.Equals(testEntity));
        }

        [Fact]
        public void SaveOrUpdate_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.Resolve<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            var testEntity = new ParentEntity().GenerateForTest();

            _sut.SaveOrUpdate(testEntity, tc);

            tc.Dispose();
        }

        #endregion

        #region Delete

        [Fact]
        public void Delete_Removes_Record_As_Expected()
        {
            var testEntity = new ParentEntity().SaveForTest();

            _sut.Delete(testEntity);

            var result = _sut.GetById(testEntity.Id);

            Assert.Null(result);
        }

        [Fact]
        public void Delete_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.Resolve<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            var testEntity = new ParentEntity().SaveForTest(tc);

            _sut.Delete(testEntity, tc);

            tc.Dispose();
        }

        #endregion

        #region DeleteById

        [Fact]
        public void DeleteById_Removes_Records_As_Expected()
        {
            var testEntity = new ParentEntity().SaveForTest();

            _sut.DeleteById(testEntity.Id);

            var result = _sut.GetById(testEntity.Id);

            Assert.Null(result);
        }

        [Fact]
        public void DeleteById_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.Resolve<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            var testEntity = new ParentEntity().SaveForTest(tc);

            _sut.DeleteById(testEntity.Id, tc);

            tc.Dispose();
        }

        #endregion
    }
}
