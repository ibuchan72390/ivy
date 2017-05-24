using Ivy.Caching.IoC;
using Ivy.IoC.IoC;
using Xunit;

namespace Ivy.IoC.Test
{
    public class InstallerTests
    {
        [Fact]
        public void IoC_Installer_Can_Install_Appropriately()
        {
            var containerGen = new ContainerGenerator();

            containerGen.InstallIvyIoC();

            var continaer = containerGen.GenerateContainer();
        }

        [Fact]
        public void Caching_Installer_Can_Install_Appropriately()
        {
            var containerGen = new ContainerGenerator();

            containerGen.InstallIvyCaching();

            var continaer = containerGen.GenerateContainer();
        }
    }
}
