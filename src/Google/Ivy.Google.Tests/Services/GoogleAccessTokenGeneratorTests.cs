using Ivy.Google.Core.Interfaces.Providers;
using Ivy.Google.Tests.Base;
using Ivy.Google.Core.Interfaces.Services;
using Ivy.IoC;
using Ivy.IoC.Core;
using Moq;
using Xunit;

namespace Ivy.Google.Tests.Services
{
    public class GoogleAccessTokenGeneratorTests : GoogleTestBase
    {
        /*
         * I honestly have absolutely no idea how we can test this service
         * It is simply invoking a static Google class' child functions.
         * 
         * If you figure out how to make this work, put some tests here.
         * 
         * At the very least, I can test the container resolution to ensure
         * the container installer is configured properly.
         */

        [Fact] // This should at least test the IvyGoogleCoreInstaller
        public void GoogleAccessTokenGenerator_Resolves_Correctly()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            var configMock = new Mock<IGoogleConfigurationProvider>();
            containerGen.RegisterInstance<IGoogleConfigurationProvider>(configMock.Object);

            var result = containerGen.GenerateContainer().GetService<IGoogleAccessTokenGenerator>();

            Assert.NotNull(result);
        }
    }
}
