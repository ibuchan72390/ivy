using IBFramework.Caching.IoC;
using IBFramework.IoC.IoC;
using Xunit;

namespace IBFramework.IoC.Test
{
    public class InstallerTests
    {
        [Fact]
        public void IoC_Installer_Can_Install_Appropriately()
        {
            var containerGen = new ContainerGenerator();

            containerGen.InstallIoC();

            var continaer = containerGen.GenerateContainer();
        }

        [Fact]
        public void Caching_Installer_Can_Install_Appropriately()
        {
            var containerGen = new ContainerGenerator();

            containerGen.InstallCaching();

            var continaer = containerGen.GenerateContainer();
        }
    }
}
