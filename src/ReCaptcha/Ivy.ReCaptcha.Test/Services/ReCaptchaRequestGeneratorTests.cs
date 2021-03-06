﻿using Ivy.IoC;
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
    public class ReCaptchaRequestGeneratorTests : 
        ReCaptchaTestBase<IReCaptchaRequestGenerator>
    {
        #region Variables & Constants

        private Mock<IReCaptchaRequestContentGenerator> _mockContentGen;
        private Mock<IReCaptchaConfigurationProvider> _mockConfig;
        
        #endregion

        #region Setup & Teardown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockContentGen = InitializeMoq<IReCaptchaRequestContentGenerator>(containerGen);

            _mockConfig = InitializeMoq<IReCaptchaConfigurationProvider>(containerGen);
            _mockConfig.Setup(x => x.ValidationUrl).Returns("https://www.google.com/recaptcha/api/siteverify");
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

            var req = Sut.GenerateValidationRequest(reCaptchaCode, remoteIp);

            _mockContentGen.Verify(x => x.GenerateValidationKeyPairs(reCaptchaCode, remoteIp), Times.Once);

            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal("https://www.google.com/recaptcha/api/siteverify", req.RequestUri.AbsoluteUri.ToString());
        }

        #endregion
    }
}
