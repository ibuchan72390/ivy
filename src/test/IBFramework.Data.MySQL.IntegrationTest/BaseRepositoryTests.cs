using IBFramework.Core.Data;
using IBFramework.IoC;
using IBFramework.TestHelper;
using IBFramework.TestHelper.TestEntities;
using IBFramework.TestHelper.TestValues;
using System;
using System.Linq;
using Xunit;

namespace IBFramework.Data.MySQL.IntegrationTest
{
    public class BaseRepositoryTests : MySqlIntegrationTestBase, IDisposable
    {
        #region Variables & Constants

        private IBlobRepository<BlobEntity> _sut;

        #endregion

        #region Constructor

        public BaseRepositoryTests()
        {
            _sut = ServiceLocator.Instance.Resolve<IBlobRepository<BlobEntity>>();
            _sut.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);
        }

        public void Dispose()
        {
            _sut.DeleteAll();
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
