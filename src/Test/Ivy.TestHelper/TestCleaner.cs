using Ivy.Data.Core.Interfaces;
using Ivy.IoC;
using Ivy.TestHelper.TestEntities;
using Ivy.TestHelper.TestEntities.Flipped;
using Ivy.TestHelper.TestValues;

namespace Ivy.TestHelper
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
