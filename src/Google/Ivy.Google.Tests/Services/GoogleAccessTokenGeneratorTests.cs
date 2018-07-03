using Ivy.Google.Core.Interfaces.Providers;
using Ivy.Google.Tests.Base;
using Ivy.Google.Core.Interfaces.Services;
using Ivy.IoC;
using Ivy.IoC.Core;
using Moq;
using Xunit;

/*
 * I honestly have absolutely no idea how we can test this service
 * It is simply invoking a static Google class' child functions.
 * 
 * If you figure out how to make this work, put some tests here.
 * 
 * At the very least, I can test the container resolution to ensure
 * the container installer is configured properly.
 */

namespace Ivy.Google.Tests.Services
{
    public class GoogleAccessTokenGeneratorTests : 
        GoogleTestBase<IGoogleAccessTokenGenerator>
    {
        #region Variables & Constants

        private Mock<IGoogleConfigurationProvider> _mockConfig;

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockConfig = InitializeMoq<IGoogleConfigurationProvider>(containerGen);
        }

        #endregion

        [Fact] // This should at least test the IvyGoogleCoreInstaller
        public void GoogleAccessTokenGenerator_Resolves_Correctly()
        {
            Assert.NotNull(Sut);
        }
    }
}
