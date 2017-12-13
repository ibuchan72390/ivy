using Ivy.Data.Common.Pagination;
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
    public class BaseRepositoryTests : MySqlIntegrationTestBase, IDisposable
    {
        #region Variables & Constants

        private IBlobRepository<BlobEntity> _sut;

        #endregion

        #region Constructor

        public BaseRepositoryTests()
        {
            _sut = ServiceLocator.Instance.GetService<IBlobRepository<BlobEntity>>();
            _sut.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);
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

            var result = _sut.GetAll(paginationRequest);

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

            var results = _sut.GetAll();

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

            var results = _sut.GetAll();

            Assert.Equal(itemsToMake, results.Count());

            _sut.DeleteAll();

            results = _sut.GetAll();

            Assert.Empty(results);
        }

        #endregion
    }
}
