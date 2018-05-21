using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.ReCaptcha.Core.Interfaces.Providers;
using Ivy.ReCaptcha.Core.Interfaces.Services;
using Ivy.ReCaptcha.Test.Base;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace Ivy.ReCaptcha.Test.Services
{
    public class ReCaptchaRequestGeneratorTests : ReCaptchaTestBase
    {
        #region Variables & Constants

        private readonly IReCaptchaRequestGenerator _sut;

        private readonly Mock<IReCaptchaRequestContentGenerator> _mockContentGen;
        private readonly Mock<IReCaptchaConfigurationProvider> _mockConfig;
        
        #endregion

        #region Setup & Teardown

        public ReCaptchaRequestGeneratorTests()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockContentGen = new Mock<IReCaptchaRequestContentGenerator>();
            containerGen.RegisterInstance<IReCaptchaRequestContentGenerator>(_mockContentGen.Object);

            _mockConfig = new Mock<IReCaptchaConfigurationProvider>();
            _mockConfig.Setup(x => x.ValidationUrl).Returns("https://www.google.com/recaptcha/api/siteverify");
            containerGen.RegisterInstance<IReCaptchaConfigurationProvider>(_mockConfig.Object);

            _sut = containerGen.GenerateContainer().GetService<IReCaptchaRequestGenerator>();
        }

        #endregion

        #region Tests

        [Fact]
        public void GenerateRequest_Assigns_Values_As_Expected_With_No_RemoteIp()
        {
            string reCaptchaCode = "code";
            string remoteIp = "remoteIp";

            var content = new List<KeyValuePair<string, string>>();

            _mockContentGen.Setup(x => x.GenerateValidationKeyPairs(reCaptchaCode, remoteIp)).Returns(content);

            var req = _sut.GenerateValidationRequest(reCaptchaCode, remoteIp);

            _mockContentGen.Verify(x => x.GenerateValidationKeyPairs(reCaptchaCode, remoteIp), Times.Once);

            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal("https://www.google.com/recaptcha/api/siteverify", req.RequestUri.AbsoluteUri.ToString());
        }

        #endregion
    }
}
