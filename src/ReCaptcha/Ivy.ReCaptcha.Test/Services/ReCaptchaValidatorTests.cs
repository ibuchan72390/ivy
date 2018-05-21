using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.ReCaptcha.Core.Interfaces.Models;
using Ivy.ReCaptcha.Core.Interfaces.Services;
using Ivy.ReCaptcha.Core.Models;
using Ivy.ReCaptcha.Test.Base;
using Ivy.Web.Core.Client;
using Moq;
using System.Net.Http;
using Xunit;

namespace Ivy.ReCaptcha.Test.Services
{
    public class ReCaptchaValidatorTests : ReCaptchaTestBase
    {
        #region Variables & Constants

        private readonly IReCaptchaValidator _sut;

        private readonly Mock<IReCaptchaRequestGenerator> _mockRequestGen;
        private readonly Mock<IApiHelper> _mockApiHelper;

        #endregion

        #region Setup & Teardown

        public ReCaptchaValidatorTests()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockRequestGen = new Mock<IReCaptchaRequestGenerator>();
            containerGen.RegisterInstance<IReCaptchaRequestGenerator>(_mockRequestGen.Object);

            _mockApiHelper = new Mock<IApiHelper>();
            containerGen.RegisterInstance<IApiHelper>(_mockApiHelper.Object);

            _sut = containerGen.GenerateContainer().GetService<IReCaptchaValidator>();
        }

        #endregion

        #region Tests

        [Fact]
        public async void Validate_Executes_As_Expected()
        {
            const string code = "code";
            const string ip = "ip";

            var req = new HttpRequestMessage();

            var mockResult = new ReCaptchaResponse();

            _mockRequestGen.Setup(x => x.GenerateValidationRequest(code, ip)).Returns(req);

            _mockApiHelper.Setup(x => x.GetApiDataAsync<IReCaptchaResponse>(req)).ReturnsAsync(mockResult);

            var result = await _sut.ValidateAsync<IReCaptchaResponse>(code, ip);

            Assert.Same(mockResult, result);

            _mockRequestGen.Verify(x => x.GenerateValidationRequest(code, ip), Times.Once);
            _mockApiHelper.Verify(x => x.GetApiDataAsync<IReCaptchaResponse>(req), Times.Once);
        }

        #endregion
    }
}
