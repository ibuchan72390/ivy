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
    public class ReCaptchaValidatorTests : 
        ReCaptchaTestBase<IReCaptchaValidator>
    {
        #region Variables & Constants

        private Mock<IReCaptchaRequestGenerator> _mockRequestGen;
        private Mock<IApiHelper> _mockApiHelper;

        #endregion

        #region Setup & Teardown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockRequestGen = InitializeMoq<IReCaptchaRequestGenerator>(containerGen);
            _mockApiHelper = InitializeMoq<IApiHelper>(containerGen);
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

            var result = await Sut.ValidateAsync<IReCaptchaResponse>(code, ip);

            Assert.Same(mockResult, result);

            _mockRequestGen.Verify(x => x.GenerateValidationRequest(code, ip), Times.Once);
            _mockApiHelper.Verify(x => x.GetApiDataAsync<IReCaptchaResponse>(req), Times.Once);
        }

        #endregion
    }
}
