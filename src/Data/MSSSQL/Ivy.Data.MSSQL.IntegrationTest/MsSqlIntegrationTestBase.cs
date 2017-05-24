using Ivy.Core.Data;
using Ivy.Data.Common.IoC;
using Ivy.Data.MSSQL.IoC;
using Ivy.IoC;
using Ivy.TestHelper;
using Ivy.TestHelper.TestValues;

namespace Ivy.Data.MSSQL.IntegrationTest
{
    public class MsSqlIntegrationTestBase : TestBase
    {
        public MsSqlIntegrationTestBase()
        {
            Init(container => {
                container.InstallCommonData();
                container.InstallMsSql();

                TestExtensions.Init(MsSqlTestValues.TestDbConnectionString);
            });
        }

        public ITranConn TestTranConn =>
            ServiceLocator.Instance.Resolve<ITranConnGenerator>()
                .GenerateTranConn(MsSqlTestValues.TestDbConnectionString);

    }
}
