using IBFramework.Data.Core.Interfaces;
using IBFramework.IoC;
using IBFramework.TestHelper;
using IBFramework.TestHelper.TestEntities;
using IBFramework.TestHelper.TestEntities.Flipped;
using IBFramework.TestHelper.TestValues;
using IBFramework.Utility.Extensions;
using System;
using System.Linq;
using Xunit;

namespace IBFramework.Data.MySQL.IntegrationTest
{
    public class EntityReferencesTests : MySqlIntegrationTestBase, IDisposable
    {
        #region SetUp & TearDown

        public void Dispose()
        {
            TestCleaner.CleanDatabase();
        }

        #endregion

        #region Tests

        #region Reference Population

        [Fact]
        public void ReferencesCollection_Properly_Populates_Id_Values_For_Standard_Int_Entity()
        {
            var coreEntity = new CoreEntity().SaveForTest();
            var childEntity = new ChildEntity { CoreEntity = coreEntity }.SaveForTest();

            var repo = ServiceLocator.Instance.Resolve<IEntityRepository<ChildEntity>>();
            repo.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);

            var result = repo.GetById(childEntity.Id);

            const string expectedReferenceKey = "CoreEntityId";

            var referenceFromCollection = result.References[expectedReferenceKey];
            var referenceAsId = (int)referenceFromCollection;

            Assert.Equal(coreEntity.Id, referenceAsId);
        }

        [Fact]
        public void ReferencesCollection_Properly_Populates_Id_Values_For_Blob_Entity()
        {
            var coreEntity = new CoreEntity().SaveForTest();
            var fbEntity = new FlippedBlobEntity { CoreEntity = coreEntity }.SaveForTest();

            var repo = ServiceLocator.Instance.Resolve<IBlobRepository<FlippedBlobEntity>>();
            repo.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);

            var items = repo.GetAll();

            Assert.Equal(1, items.Count());

            var item = items.First();

            var coreId = item.SafeGetIntRef(x => x.CoreEntity);

            Assert.Equal(coreEntity.Id, coreId);
        }

        #endregion

        #region SafeGetIntRef

        [Fact]
        public void SafeGetIntRef_Works_As_Expected()
        {
            var coreEntity = new CoreEntity().SaveForTest();
            var childEntity = new ChildEntity { CoreEntity = coreEntity }.SaveForTest();

            var repo = ServiceLocator.Instance.Resolve<IEntityRepository<ChildEntity>>();
            repo.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);

            var result = repo.GetById(childEntity.Id);

            var referenceAsId = result.SafeGetIntRef(x => x.CoreEntity);

            Assert.Equal(coreEntity.Id, referenceAsId);
        }

        #endregion

        #region SafeGetStringRef

        [Fact]
        public void SafeGetStringRef_Works_As_Expected()
        {
            var fsEntity = new FlippedStringEntity().SaveForTest();
            var coreEntity = new CoreEntity { FlippedStringEntity = fsEntity }.SaveForTest();

            var repo = ServiceLocator.Instance.Resolve<IEntityRepository<CoreEntity>>();
            repo.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);

            var result = repo.GetById(coreEntity.Id);

            var referenceAsId = result.SafeGetStringRef(x => x.FlippedStringEntity);

            Assert.Equal(fsEntity.Id, referenceAsId);
        }

        #endregion

        #endregion
    }
}
