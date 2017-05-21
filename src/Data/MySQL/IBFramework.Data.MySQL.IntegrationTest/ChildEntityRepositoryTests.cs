using IBFramework.Data.Core.Interfaces;
using IBFramework.IoC;
using IBFramework.TestHelper;
using IBFramework.TestHelper.TestEntities;
using IBFramework.TestHelper.TestValues;
using System;
using Xunit;

namespace IBFramework.Data.MySQL.IntegrationTest
{
    /*
     * These tests ensure that we're properly assigning the FK properly
     * Since ChildEntity has a reference to CoreEntity, we'll want it to
     * properly assign the CoreEntityId wherever possible
     */
    public class ChildEntityRepositoryTests : MySqlIntegrationTestBase, IDisposable
    {
        #region Variables & Constants

        private IEntityRepository<ChildEntity, int> _sut;

        #endregion

        #region Constructor

        public ChildEntityRepositoryTests()
        {
            _sut = ServiceLocator.Instance.Resolve<IEntityRepository<ChildEntity, int>>();

            _sut.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);
        }

        public void Dispose()
        {
            TestCleaner.CleanDatabase();
        }

        #endregion

        #region Tests

        [Fact]
        public void ChildEntity_Save_Properly_Assigns_CoreEntityId_Value()
        {
            var childEntity = new ChildEntity().GenerateForTest();

            var coreEntity = new CoreEntity().SaveForTest();

            childEntity.CoreEntity = coreEntity;

            _sut.SaveOrUpdate(childEntity);

            var tranGenerator = ServiceLocator.Instance.Resolve<ITranConnGenerator>();
            var tran = tranGenerator.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            var cmd = tran.Connection.CreateCommand();
            cmd.CommandText = $"SELECT CoreEntityId FROM ChildEntity WHERE Id = {childEntity.Id}";

            var results = cmd.ExecuteReader();

            while (results.Read())
            {
                var resultId = results.GetValue(0);
                Assert.Equal(coreEntity.Id, resultId);
            }

            results.Close();
        }

        #endregion
    }
}
