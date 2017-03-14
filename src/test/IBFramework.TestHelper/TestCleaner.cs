using IBFramework.Core.Data;
using IBFramework.IoC;
using IBFramework.TestHelper.TestEntities;
using IBFramework.TestHelper.TestValues;

namespace IBFramework.TestHelper
{
    public static class TestCleaner
    {
        public static void CleanDatabase()
        {
            var repo = ServiceLocator.Instance.Resolve<IBlobRepository<BlobEntity>>();

            repo.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);
        }
    }
}
