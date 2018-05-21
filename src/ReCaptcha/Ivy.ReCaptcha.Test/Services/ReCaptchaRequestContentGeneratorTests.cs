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
    public class ReCaptchaRequestContentGeneratorTests : ReCaptchaTestBase
    {
        #region Variables & Constants

        private readonly IReCaptchaRequestContentGenerator _sut;

        private readonly Mock<IReCaptchaConfigurationProvider> _mockConfig;

        const string Secret = "Secret";
        const string Code = "Code";

        #endregion

        #region Setup & Teardown
        
        public ReCaptchaRequestContentGeneratorTests()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockConfig = new Mock<IReCaptchaConfigurationProvider>();
            _mockConfig.Setup(x => x.Secret).Returns(Secret);
            containerGen.RegisterInstance<IReCaptchaConfigurationProvider>(_mockConfig.Object);

            _sut = containerGen.GenerateContainer().GetService<IReCaptchaRequestContentGenerator>();
        }

        #endregion

        #region Tests

        [Fact]
        public void GenerateValidationKeyPairs_Works_As_Expected_Without_RemoteIp()
        {
            var results = _sut.GenerateValidationKeyPairs(Code);

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

            var results = _sut.GenerateValidationKeyPairs(Code, remoteIp);

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
