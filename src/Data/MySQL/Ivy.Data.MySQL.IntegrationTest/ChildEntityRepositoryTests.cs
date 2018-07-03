using Ivy.Data.Core.Interfaces;
using Ivy.IoC;
using Ivy.TestHelper;
using Ivy.TestHelper.TestEntities;
using Ivy.TestHelper.TestValues;
using System;
using Xunit;

namespace Ivy.Data.MySQL.IntegrationTest
{
    /*
     * These tests ensure that we're properly assigning the FK properly
     * Since ChildEntity has a reference to CoreEntity, we'll want it to
     * properly assign the CoreEntityId wherever possible
     */
    public class ChildEntityRepositoryTests : 
        MySqlIntegrationTestBase<IEntityRepository<ChildEntity, int>>, 
        IDisposable
    {
        #region Constructor

        public ChildEntityRepositoryTests()
        {
            Sut.InitializeByConnectionString(MySqlTestValues.TestDbConnectionString);
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

            Sut.SaveOrUpdate(childEntity);

            var tranGenerator = ServiceLocator.Instance.GetService<ITranConnGenerator>();

            // Make sure to wrap in "using", failure to dispose transaction is dangerous
            using (var tran = tranGenerator.GenerateTranConn(MySqlTestValues.TestDbConnectionString))
            {
                var cmd = tran.Connection.CreateCommand();
                cmd.CommandText = $"SELECT CoreEntityId FROM ChildEntity WHERE Id = {childEntity.Id}";

                using (var results = cmd.ExecuteReader())
                {
                    while (results.Read())
                    {
                        var resultId = results.GetValue(0);
                        Assert.Equal(coreEntity.Id, resultId);
                    }

                    results.Close();
                }
            }
        }

        #endregion
    }
}
