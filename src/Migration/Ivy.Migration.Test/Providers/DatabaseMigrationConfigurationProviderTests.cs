using Ivy.IoC;
using Ivy.Migration.Core.Interfaces.Providers;
using Ivy.Migration.Providers;
using Ivy.Migration.Test.Base;
using Xunit;

namespace Ivy.Migration.Test.Providers
{
    public class DatabaseMigrationConfigurationProviderTests :
        MigrationTestBase<IDatabaseMigrationConfigurationProvider>
    {
        [Fact]
        public void Default_Config_Is_Registered_Appropriately()
        {
            Assert.NotNull(Sut);

            var sutType = Sut.GetType();

            Assert.Equal(typeof(DefaultDatabaseMigrationConfigurationProvider), sutType);
        }

        [Fact]
        public void Default_Config_Overrides_Appropriately()
        {
            var containerGen = new ContainerGenerator();

            base.InitializeContainerFn(containerGen);

            containerGen.RegisterSingleton<IDatabaseMigrationConfigurationProvider, TestMigrationConfigurationProvider>();

            var result = containerGen.GenerateContainer().GetService<IDatabaseMigrationConfigurationProvider>();

            var resultType = result.GetType();

            Assert.Equal(typeof(TestMigrationConfigurationProvider), resultType);
        }
    }

    public class TestMigrationConfigurationProvider : IDatabaseMigrationConfigurationProvider
    {
        public string LoginUserName => "MyTestLoginUserName";

        public string LoginPassword => "MyTestLoginPassword";
    }
}
