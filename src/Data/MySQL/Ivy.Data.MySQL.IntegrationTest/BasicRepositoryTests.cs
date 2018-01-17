﻿using Ivy.Data.Core.Interfaces;
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
    public class BasicRepositoryTests : MySqlIntegrationTestBase, IDisposable
    {
        #region Variables & Constants

        private IBlobRepository<BlobEntity> _sut;

        #endregion

        #region Setup

        public BasicRepositoryTests()
        {
            _sut = ServiceLocator.Instance.GetService<IBlobRepository<BlobEntity>>();

            _sut.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);
        }

        public void Dispose()
        {
            TestCleaner.CleanDatabase();
        }

        #endregion

        #region Tests

        #region Insert

        [Fact]
        public void Insert_Creates_New_Record_In_Database()
        {
            var entity = new BlobEntity().GenerateForTest();

            _sut.Insert(entity);

            var results = _sut.GetAll().ToList();

            Assert.Single(results);

            var result = results[0];

            Assert.True(result.Equals(entity));
        }

        [Fact]
        public void Insert_Fails_To_Create_If_Transaction_Rolled_Back()
        {
            var entity = new BlobEntity().GenerateForTest();

            var tranGenerator = ServiceLocator.Instance.GetService<ITranConnGenerator>();
            var tran = tranGenerator.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            _sut.Insert(entity, tran);
            tran.Transaction.Rollback();

            tran.Dispose();

            var results = _sut.GetAll().ToList();

            Assert.Empty(results);
        }

        #endregion

        #region Bulk Insert

        [Fact]
        public void Bulk_Insert_Creates_Many_New_Record_In_Database()
        {
            const int toCreate = 5;

            var entities = Enumerable.Range(0, toCreate).Select(x => new BlobEntity().GenerateForTest()).ToList();

            _sut.BulkInsert(entities);

            var results = _sut.GetAll().ToList();

            Assert.Equal(toCreate, results.Count());

            // Enforce order by sorting instead of ids
            Func<IEnumerable<BlobEntity>, IEnumerable<BlobEntity>> blobEntitySort = 
                y => y.OrderBy(x => x.Name).ThenBy(x => x.Integer).ThenBy(x => x.Decimal).ThenBy(x => x.Double);

            var entitiesList = blobEntitySort(entities).ToList();
            results = blobEntitySort(results).ToList();

            for (var i = 0; i < toCreate; i++)
            {
                var testEntity = entitiesList[i];
                var testResult = results[i];

                var equal = testEntity.Equals(testResult);

                Assert.True(equal);
            }
        }

        [Fact]
        public void Bulk_Insert_Fails_To_Create_If_Transaction_Rolled_Back()
        {
            var entities = Enumerable.Range(0, 5).Select(x => new BlobEntity().GenerateForTest()).ToList();

            var tranGenerator = ServiceLocator.Instance.GetService<ITranConnGenerator>();
            var tc = tranGenerator.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            _sut.BulkInsert(entities, tc);
            tc.Transaction.Rollback();

            tc.Dispose();

            var results = _sut.GetAll().ToList();

            Assert.Empty(results);
        }

        #endregion

        #endregion
    }
}
