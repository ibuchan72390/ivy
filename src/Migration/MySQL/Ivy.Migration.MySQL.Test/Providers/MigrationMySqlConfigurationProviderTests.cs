using Ivy.IoC;
using Ivy.Migration.MySQL.Core.Providers;
using Ivy.Migration.MySQL.Providers;
using Ivy.Migration.MySQL.Test.Base;
using Xunit;

namespace Ivy.Migration.MySQL.Test.Providers
{
    public class MigrationMySqlConfigurationProviderTests :
        MySqlMigrationTestBase<IMySqlMigrationConfigurationProvider>
    {
        #region Tests

        [Fact]
        public void Configuration_Resolves_As_Expected()
        {
            Assert.NotNull(Sut);

            var expected = new DefaultMySqlMigrationConfigurationProvider();

            Assert.Equal(expected.Delimiter, Sut.Delimiter);
        }

        [Fact]
        public void Override_Works_As_Expected()
        {
            var containerGen = new ContainerGenerator();

            base.InitializeContainerFn(containerGen);

            containerGen.RegisterSingleton<IMySqlMigrationConfigurationProvider, TestMySqlMigrationConfigurationProvider>();

            var result = containerGen.GenerateContainer().GetService<IMySqlMigrationConfigurationProvider>();

            var expected = new TestMySqlMigrationConfigurationProvider();

            Assert.Equal(expected.Delimiter, result.Delimiter);
        }

        #endregion
    }

    public class TestMySqlMigrationConfigurationProvider : IMySqlMigrationConfigurationProvider
    {
        public string Delimiter => "TEST";
    }
}
