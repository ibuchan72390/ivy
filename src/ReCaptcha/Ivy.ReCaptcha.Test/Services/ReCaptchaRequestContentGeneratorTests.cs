using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.ReCaptcha.Core.Interfaces.Providers;
using Ivy.ReCaptcha.Core.Interfaces.Services;
using Ivy.ReCaptcha.Test.Base;
using Moq;
using System.Linq;
using Xunit;

namespace Ivy.ReCaptcha.Test.Services
{
    public class ReCaptchaRequestContentGeneratorTests : 
        ReCaptchaTestBase<IReCaptchaRequestContentGenerator>
    {
        #region Variables & Constants

        private Mock<IReCaptchaConfigurationProvider> _mockConfig;

        const string Secret = "Secret";
        const string Code = "Code";

        #endregion

        #region Setup & Teardown
        
        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockConfig = InitializeMoq<IReCaptchaConfigurationProvider>(containerGen);
            _mockConfig.Setup(x => x.Secret).Returns(Secret);
        }

        #endregion

        #region Tests

        [Fact]
        public void GenerateValidationKeyPairs_Works_As_Expected_Without_RemoteIp()
        {
            var results = Sut.GenerateValidationKeyPairs(Code);

            Assert.Equal(2, results.Count);

            var secretKey = results.First(x => x.Key == "secret");
            Assert.Equal(Secret, secretKey.Value);

            var responseKey = results.First(x => x.Key == "response");
            Assert.Equal(Code, responseKey.Value);
        }

        [Fact]
        public void GenerateValidationKeyPairs_Works_As_Expected_With_RemoteIp()
        {
            const string remoteIp = "RemoteIp";

            var results = Sut.GenerateValidationKeyPairs(Code, remoteIp);

            Assert.Equal(3, results.Count);

            var secretKey = results.First(x => x.Key == "secret");
            Assert.Equal(Secret, secretKey.Value);

            var responseKey = results.First(x => x.Key == "response");
            Assert.Equal(Code, responseKey.Value);

            var ipKey = results.First(x => x.Key == "remoteip");
            Assert.Equal(remoteIp, ipKey.Value);
        }

        #endregion
    }
}
