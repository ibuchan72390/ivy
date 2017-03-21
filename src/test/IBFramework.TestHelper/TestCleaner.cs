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
            CleanTable<BlobEntity>();
            CleanTable<StringIdEntity>();
            CleanTable<GuidIdEntity>();
            CleanTable<ChildEntity>();
            CleanTable<CoreEntity>();
            CleanTable<ParentEntity>();
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
