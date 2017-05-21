using IBFramework.Data.Core.Interfaces;
using IBFramework.IoC;
using IBFramework.TestHelper.TestEntities;
using IBFramework.TestHelper.TestEntities.Flipped;
using IBFramework.TestHelper.TestValues;

namespace IBFramework.TestHelper
{
    public static class TestCleaner
    {
        public static void CleanDatabase()
        {
            CleanTable<BlobEntity>();
            CleanTable<StringEntity>();
            CleanTable<ChildEntity>();
            CleanTable<FlippedBlobEntity>();
            CleanTable<CoreEntity>();
            CleanTable<ParentEntity>();
            CleanTable<FlippedStringEntity>();
            CleanTable<TestEnumEntity>();
        }

        public static void CleanTable<T>()
            where T : class
        {
            var repo = ServiceLocator.Instance.Resolve<IBlobRepository<T>>();
            repo.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);
            repo.DeleteAll();
        }
    }
}
