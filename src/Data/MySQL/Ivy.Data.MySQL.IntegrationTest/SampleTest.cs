using Dapper;
using Ivy.Data.Core.Interfaces;
using Ivy.Data.Core.Interfaces.SQL;
using Ivy.TestHelper;
using Ivy.TestHelper.TestEntities;
using Ivy.TestHelper.TestValues;
using System.Threading.Tasks;
using Xunit;

namespace Ivy.Data.MySQL.IntegrationTest
{
    public class SampleTest : MySqlIntegrationTestBase<IEntityRepository<TestEnumEntity>>
    {
        [Fact]
        public async Task does_this_fucking_work()
        {
            var entity = new TestEnumEntity().SaveForTest();

            var tcGen = TestContainer.GetService<ITranConnGenerator>();
            var sqlGen = TestContainer.GetService<ISqlGenerator<TestEnumEntity>>();

            var tc = tcGen.GenerateTranConn(MySqlTestValues.TestDbConnectionString);

            var sql = sqlGen.GenerateGetQuery();

            using (var reader = await tc.Connection.ExecuteReaderAsync(sql, null))
            {
                Assert.True(reader.FieldCount > 0);
            }
        }
    }
}
