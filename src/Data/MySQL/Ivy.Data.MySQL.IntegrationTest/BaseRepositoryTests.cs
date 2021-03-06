﻿using Ivy.Data.Common.Pagination;
using Ivy.Data.Core.Interfaces;
using Ivy.TestHelper;
using Ivy.TestHelper.TestEntities;
using Ivy.TestHelper.TestValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ivy.Data.MySQL.IntegrationTest
{
    public class BaseRepositoryTests : 
        MySqlIntegrationTestBase<IBlobRepository<BlobEntity>>, 
        IDisposable
    {
        #region Constructor

        public BaseRepositoryTests()
        {
            Sut.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);
        }

        public void Dispose()
        {
            TestCleaner.CleanDatabase();
        }

        #endregion

        #region GetAll (Paginated)

        [Fact]
        public void GetAll_Paginated_Returns_As_Expected_For_First_Page()
        {
            TestPagination(5, 13, 1);
        }

        [Fact]
        public void GetAll_Paginated_Returns_As_Expected_For_Second_Page()
        {
            TestPagination(5, 13, 2);
        }

        [Fact]
        public void GetAll_Paginated_Returns_As_Expected_For_Last_Page_With_Partial_Data()
        {
            TestPagination(5, 13, 3);
        }

        private void TestPagination(int pageCount, int toSave, int pageNumber)
        {
            int takeLow = (pageNumber - 1) * pageCount;
            int takeHigh = pageNumber * pageCount;

            var entities = Enumerable.Range(0, toSave).
                Select(x => new BlobEntity().SaveForTest()).
                ToList();

            IList<BlobEntity> expected = new List<BlobEntity>();
            for (var i = 0; i < entities.Count; i++)
            {
                if (i >= takeLow && i < takeHigh)
                {
                    expected.Add(entities[i]);
                }
            }

            var paginationRequest = new PaginationRequest
            {
                PageNumber = pageNumber,
                PageCount = pageCount
            };

            var result = Sut.GetAll(paginationRequest);

            Assert.Equal(toSave, result.TotalCount);

            Assert.Equal(expected.Count(), result.Data.Count());
            
            foreach (var item in expected)
            {
                var matching = result.Data.FirstOrDefault(x => x.Name == item.Name);
                Assert.True(item.Equals(matching));
            }
        }

        #endregion

        #region GetAllAsync (Paginated)

        [Fact]
        public async void GetAllAsync_Paginated_Returns_As_Expected_For_First_Page()
        {
            await TestPaginationAsync(5, 13, 1);
        }

        [Fact]
        public async void GetAllAsync_Paginated_Returns_As_Expected_For_Second_Page()
        {
            await TestPaginationAsync(5, 13, 2);
        }

        [Fact]
        public async void GetAllAsync_Paginated_Returns_As_Expected_For_Last_Page_With_Partial_Data()
        {
            await TestPaginationAsync(5, 13, 3);
        }

        private async Task TestPaginationAsync(int pageCount, int toSave, int pageNumber)
        {
            int takeLow = (pageNumber - 1) * pageCount;
            int takeHigh = pageNumber * pageCount;

            var entities = Enumerable.Range(0, toSave).
                Select(x => new BlobEntity().SaveForTest()).
                ToList();

            IList<BlobEntity> expected = new List<BlobEntity>();
            for (var i = 0; i < entities.Count; i++)
            {
                if (i >= takeLow && i < takeHigh)
                {
                    expected.Add(entities[i]);
                }
            }

            var paginationRequest = new PaginationRequest
            {
                PageNumber = pageNumber,
                PageCount = pageCount
            };

            var result = await Sut.GetAllAsync(paginationRequest);

            Assert.Equal(toSave, result.TotalCount);

            Assert.Equal(expected.Count(), result.Data.Count());

            foreach (var item in expected)
            {
                var matching = result.Data.FirstOrDefault(x => x.Name == item.Name);
                Assert.True(item.Equals(matching));
            }
        }

        #endregion

        #region GetAll

        [Fact]
        public void GetAll_Returns_All_Items_In_Table()
        {
            const int itemsToMake = 5;

            var blobs = Enumerable.Range(0, itemsToMake).Select(x => new BlobEntity().SaveForTest()).ToList();

            var results = Sut.GetAll();

            Assert.Equal(itemsToMake, results.Count());

            foreach (var item in results)
            {
                var match = blobs.FirstOrDefault(x => item.Equals(x));

                Assert.NotNull(match);
            }
        }

        #endregion

        #region GetAllAsync

        [Fact]
        public async Task GetAllAsync_Returns_All_Items_In_Table()
        {
            const int itemsToMake = 5;

            var blobs = Enumerable.Range(0, itemsToMake).Select(x => new BlobEntity().SaveForTest()).ToList();

            var results = await Sut.GetAllAsync();

            Assert.Equal(itemsToMake, results.Count());

            foreach (var item in results)
            {
                var match = blobs.FirstOrDefault(x => item.Equals(x));

                Assert.NotNull(match);
            }
        }

        #endregion

        #region DeleteAll

        [Fact]
        public void DeleteAll_Removes_All_Items_In_Table()
        {
            const int itemsToMake = 5;

            var blobs = Enumerable.Range(0, itemsToMake).Select(x => new BlobEntity().SaveForTest()).ToList();

            var results = Sut.GetAll();

            Assert.Equal(itemsToMake, results.Count());

            Sut.DeleteAll();

            results = Sut.GetAll();

            Assert.Empty(results);
        }

        #endregion

        #region DeleteAllAsync

        [Fact]
        public async void DeleteAllAsync_Removes_All_Items_In_Table()
        {
            const int itemsToMake = 5;

            var blobs = Enumerable.Range(0, itemsToMake).Select(x => new BlobEntity().SaveForTest()).ToList();

            var results = await Sut.GetAllAsync();

            Assert.Equal(itemsToMake, results.Count());

            await Sut.DeleteAllAsync();

            results = await Sut.GetAllAsync();

            Assert.Empty(results);
        }

        #endregion

        #region GetCount

        [Fact]
        public void GetCount_Executes_As_Expected_If_None_Exist()
        {
            Assert.Equal(0, Sut.GetCount());
        }

        [Fact]
        public void GetCount_Executes_As_Expected_If_Entities_Exist()
        {
            const int toCreate = 4;

            Enumerable.Range(0, toCreate)
                .Select(x => new BlobEntity().SaveForTest())
                .ToList();

            Assert.Equal(toCreate, Sut.GetCount());
        }

        #endregion

        #region GetCountAsync

        [Fact]
        public async void GetCountAsync_Executes_As_Expected_If_None_Exist()
        {
            Assert.Equal(0, await Sut.GetCountAsync());
        }

        [Fact]
        public async void GetCountAsync_Executes_As_Expected_If_Entities_Exist()
        {
            const int toCreate = 4;

            Enumerable.Range(0, toCreate)
                .Select(x => new BlobEntity().SaveForTest())
                .ToList();

            Assert.Equal(toCreate, await Sut.GetCountAsync());
        }

        #endregion
    }
}
