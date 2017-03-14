using IBFramework.Core.Data;
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
    /*
     * This functionality does NOT work at the current moment!!!
     * 
     * Id values are not properly working with the GUID Id concept
     */

    public class GuidEntityRepositoryTests : MySqlIntegrationTestBase
    {
        #region Variables & Constants

        private IRepository<GuidIdEntity, Guid> _sut;

        #endregion

        #region Constructor

        public GuidEntityRepositoryTests()
        {
            _sut = ServiceLocator.Instance.Resolve<IRepository<GuidIdEntity, Guid>>();
            _sut.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);

            _sut.DeleteAll();
        }

        #endregion

        #region GetById

        [Fact]
        public void GetById_Works_As_Expected()
        {
            var entity = new GuidIdEntity().SaveForTest();

            var result = _sut.GetById(entity.Id);

            Assert.True(entity.Equals(result));
        }

        [Fact]
        public void GetById_Returns_Null_If_Doesnt_Exist()
        {
            var result = _sut.GetById(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public void GetById_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.Resolve<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            _sut.GetById(Guid.NewGuid(), tc);
        }

        #endregion

        #region GetByIdList

        [Fact]
        public void GetByIdList_Returns_As_Expected()
        {
            const int entitiesToMake = 4;

            var entities = Enumerable.Range(0, entitiesToMake).Select(x => new GuidIdEntity().SaveForTest()).ToList();

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

            var ids = Enumerable.Range(0, idsToMake).Select(x => Guid.NewGuid());

            var results = _sut.GetByIdList(ids);

            Assert.Empty(results);
        }

        [Fact]
        public void GetByIdList_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.Resolve<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            _sut.GetByIdList(new List<Guid> { Guid.NewGuid() }, tc);
        }

        #endregion

        #region SaveOrUpdate

        [Fact]
        public void SaveOrUpdate_Identifies_New_Save_Correctly()
        {
            var testEntity = new GuidIdEntity().GenerateForTest();

            var result = _sut.SaveOrUpdate(testEntity);

            Assert.True(result.Id != null);

            var secondaryResult = _sut.GetById(result.Id);

            Assert.True(result.Equals(secondaryResult));
        }

        [Fact]
        public void SaveOrUpdate_Identifies_Existing_Update_Correctly()
        {
            var testEntity = new GuidIdEntity().SaveForTest();

            Assert.True(testEntity.Id != null);

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

            var testEntity = new GuidIdEntity().GenerateForTest();

            _sut.SaveOrUpdate(testEntity, tc);
        }

        #endregion

        #region Delete

        [Fact]
        public void Delete_Removes_Record_As_Expected()
        {
            var testEntity = new GuidIdEntity().SaveForTest();

            _sut.Delete(testEntity);

            var result = _sut.GetById(testEntity.Id);

            Assert.Null(result);
        }

        [Fact]
        public void Delete_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.Resolve<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            var testEntity = new GuidIdEntity().SaveForTest();

            _sut.Delete(testEntity, tc);
        }

        #endregion

        #region DeleteById

        [Fact]
        public void DeleteById_Removes_Records_As_Expected()
        {
            var testEntity = new GuidIdEntity().SaveForTest();

            _sut.DeleteById(testEntity.Id);

            var result = _sut.GetById(testEntity.Id);

            Assert.Null(result);
        }

        [Fact]
        public void DeleteById_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.Resolve<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            var testEntity = new GuidIdEntity().SaveForTest();

            _sut.DeleteById(testEntity.Id, tc);
        }

        #endregion
    }
}
