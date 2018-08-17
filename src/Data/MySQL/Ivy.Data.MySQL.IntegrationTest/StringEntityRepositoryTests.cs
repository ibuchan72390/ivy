using Ivy.Data.Core.Interfaces;
using Ivy.IoC;
using Ivy.TestHelper;
using Ivy.TestHelper.TestEntities;
using Ivy.TestHelper.TestValues;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ivy.Data.MySQL.IntegrationTest
{
    /*
    * This functionality does NOT work at the current moment!!!
    * 
    * Id values are not properly working with the string Id concept
    */

    public class StringEntityRepositoryTests : 
        MySqlIntegrationTestBase<IEntityRepository<StringEntity, string>>, 
        IDisposable
    {
        #region Constructor

        public StringEntityRepositoryTests()
        {
            Sut.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);
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
            var entity = new StringEntity().SaveForTest();

            var result = Sut.GetById(entity.Id);

            Assert.True(entity.Equals(result));
        }

        [Fact]
        public void GetById_Returns_Null_If_Doesnt_Exist()
        {
            var result = Sut.GetById("TEST");

            Assert.Null(result);
        }

        [Fact]
        public void GetById_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.GetService<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            Sut.GetById("TEST", tc);

            tc.Dispose();
        }

        #endregion

        #region GetByIdList

        [Fact]
        public void GetByIdList_Returns_As_Expected()
        {
            const int entitiesToMake = 4;

            var entities = Enumerable.Range(0, entitiesToMake).Select(x => new StringEntity().SaveForTest()).ToList();

            var entityIds = entities.Select(x => x.Id);

            var results = Sut.GetByIdList(entityIds);

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

            var ids = Enumerable.Range(0, idsToMake).Select(x => x.ToString());

            var results = Sut.GetByIdList(ids);

            Assert.Empty(results);
        }

        [Fact]
        public void GetByIdList_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.GetService<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            Sut.GetByIdList(new List<string> { 0.ToString() }, tc);

            tc.Dispose();
        }

        #endregion

        #region SaveOrUpdate (Entity)

        [Fact]
        public void SaveOrUpdate_Entity_Identifies_New_Save_Correctly()
        {
            var testEntity = new StringEntity().GenerateForTest();

            var result = Sut.SaveOrUpdate(testEntity);

            Assert.True(result.Id != null);

            var secondaryResult = Sut.GetById(result.Id);

            Assert.True(result.Equals(secondaryResult));
        }

        [Fact]
        public void SaveOrUpdate_Entity_Identifies_Existing_Update_Correctly()
        {
            var testEntity = new StringEntity().SaveForTest();

            Assert.True(testEntity.Id != null);

            testEntity.Integer = testEntity.Integer++;

            var result = Sut.SaveOrUpdate(testEntity);

            Assert.True(result.Id == testEntity.Id);
            Assert.Equal(result.Integer, testEntity.Integer);
            Assert.True(result.Equals(testEntity));
        }

        [Fact]
        public void SaveOrUpdate_Entity_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.GetService<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            var testEntity = new StringEntity().GenerateForTest();

            Sut.SaveOrUpdate(testEntity, tc);

            tc.Dispose();
        }

        #endregion

        #region SaveOrUpdate (Entities)

        [Fact]
        public void SaveOrUpdate_Entities_Identifies_New_Save_Correctly()
        {
            var testEntities = Enumerable.Range(0, 3).
                Select(x => new StringEntity().GenerateForTest()).
                ToList();

            var results = Sut.SaveOrUpdate(testEntities);

            foreach (var result in testEntities)
            {
                Assert.True(result.Id != null);

                var secondaryResult = Sut.GetById(result.Id);

                Assert.True(result.Equals(secondaryResult));
            }

            foreach (var result in results)
            {
                Assert.True(result.Id != null);

                var secondaryResult = Sut.GetById(result.Id);

                Assert.True(result.Equals(secondaryResult));
            }
        }

        [Fact]
        public void SaveOrUpdate_Entities_Identifies_Existing_Update_Correctly()
        {
            var testEntities = Enumerable.Range(0, 3).
               Select(x => new StringEntity().SaveForTest()).
               ToList();

            var results = Sut.SaveOrUpdate(testEntities);

            foreach (var testEntity in testEntities)
            {
                var result = results.First(x => x.Id == testEntity.Id);

                Assert.True(result.Id == testEntity.Id);
                Assert.Equal(result.Integer, testEntity.Integer);
                Assert.True(result.Equals(testEntity));
            }

            foreach (var testEntity in testEntities)
            {
                var secondaryResult = Sut.GetById(testEntity.Id);

                Assert.True(secondaryResult.Id == testEntity.Id);
                Assert.Equal(secondaryResult.Integer, testEntity.Integer);
                Assert.True(secondaryResult.Equals(testEntity));
            }
        }

        [Fact]
        public void SaveOrUpdate_Entities_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.GetService<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            var testEntities = Enumerable.Range(0, 3).
               Select(x => new StringEntity().SaveForTest()).
               ToList();

            Sut.SaveOrUpdate(testEntities, tc);

            tc.Dispose();
        }

        #endregion

        #region Delete (Entity)

        [Fact]
        public void Delete_Removes_Record_As_Expected()
        {
            var testEntity = new StringEntity().SaveForTest();

            Sut.Delete(testEntity);

            var result = Sut.GetById(testEntity.Id);

            Assert.Null(result);
        }

        [Fact]
        public void Delete_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.GetService<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            var testEntity = new StringEntity().SaveForTest(tc);

            Sut.Delete(testEntity, tc);

            tc.Dispose();
        }

        #endregion

        #region Delete (Entities)

        [Fact]
        public void Delete_Entities_Removes_Record_As_Expected()
        {
            var entities = Enumerable.Range(0, 3).
                Select(x => new StringEntity().SaveForTest()).
                ToList();

            Sut.Delete(entities);

            var entityIds = entities.Select(x => x.Id);

            var result = Sut.GetByIdList(entityIds);

            Assert.Empty(result);
        }

        [Fact]
        public void Delete_Entities_Can_Commit_With_TranConn()
        {
            var tranGen = ServiceLocator.Instance.GetService<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            var entities = Enumerable.Range(0, 3).
                Select(x => new StringEntity().SaveForTest()).
                ToList();

            Sut.Delete(entities, tc);

            // Appears this must be an explicit commit
            tc.Transaction.Commit();
            tc.Dispose();

            var entityIds = entities.Select(x => x.Id);

            var result = Sut.GetByIdList(entityIds);

            Assert.Empty(result);
        }

        #endregion

        #region DeleteById

        [Fact]
        public void DeleteById_Removes_Records_As_Expected()
        {
            var testEntity = new StringEntity().SaveForTest();

            Sut.DeleteById(testEntity.Id);

            var result = Sut.GetById(testEntity.Id);

            Assert.Null(result);
        }

        [Fact]
        public void DeleteById_Can_Take_TranConn()
        {
            var tranGen = ServiceLocator.Instance.GetService<ITranConnGenerator>();

            var tc = tranGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            var testEntity = new StringEntity().SaveForTest(tc);

            Sut.DeleteById(testEntity.Id, tc);

            tc.Dispose();
        }

        #endregion

        #region DeleteByIdList

        [Fact]
        public void DeleteByIdList_Entities_Removes_Record_As_Expected()
        {
            var entities = Enumerable.Range(0, 3).
                Select(x => new StringEntity().SaveForTest()).
                ToList();

            var entityIds = entities.Select(x => x.Id);

            Sut.DeleteByIdList(entityIds);

            var result = Sut.GetByIdList(entityIds);

            Assert.Empty(result);
        }

        #endregion
    }
}
