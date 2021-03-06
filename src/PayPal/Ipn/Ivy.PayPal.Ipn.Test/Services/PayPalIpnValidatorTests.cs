﻿using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.PayPal.Ipn.Core.Interfaces.Services;
using Ivy.PayPal.Ipn.Core.Models;
using Ivy.PayPal.Ipn.Test.Base;
using Ivy.Web.Core.Client;
using Moq;
using System;
using System.Net.Http;
using Xunit;

namespace Ivy.PayPal.Ipn.Test.Services
{
    public class PayPalIpnValidatorTests : 
        PayPalTestBase<IPayPalIpnValidator>
    {
        #region Variables & Constants

        private Mock<IPayPalRequestGenerator> mockRequestGen;
        private Mock<IHttpClientHelper> mockClientHelper;

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            mockRequestGen = InitializeMoq<IPayPalRequestGenerator>(containerGen);
            mockClientHelper = InitializeMoq<IHttpClientHelper>(containerGen);
        }

        #endregion

        #region Tests

        #region GetValidationResultAsync

        [Fact]
        public async void GetValidationResultAsync_Exectues_As_Expected()
        {
            var resultContent = "RESULTContent";
            var resultStringContent = new StringContent(resultContent);

            var bodyStr = "TEST";
            var model = new PayPalIpnResponse();

            var req = new HttpRequestMessage();
            var resp = new HttpResponseMessage { Content = resultStringContent };

            mockRequestGen.Setup(x => x.GenerateValidationRequest(bodyStr, model.Test_Ipn == 1)).Returns(req);
            mockClientHelper.Setup(x => x.SendAsync(req)).ReturnsAsync(resp);

            var result = await Sut.GetValidationResultAsync(bodyStr, model);

            Assert.Equal(result, resultContent);

            mockRequestGen.Verify(x => x.GenerateValidationRequest(bodyStr, model.Test_Ipn == 1), Times.Once);
            mockClientHelper.Verify(x => x.SendAsync(req), Times.Once);
        }

        [Fact]
        public async void GetValidationResultAsync_Throws_Exception_If_Encountered()
        {
            var bodyStr = "TEST";
            var model = new PayPalIpnResponse();

            var req = new HttpRequestMessage();
            var err = new Exception();

            mockRequestGen.Setup(x => x.GenerateValidationRequest(bodyStr, model.Test_Ipn == 1)).Returns(req);
            mockClientHelper.Setup(x => x.SendAsync(req)).Throws(err);

            var result = await Assert.ThrowsAsync<Exception>(() => Sut.GetValidationResultAsync(bodyStr, model));

            Assert.Same(result, err);

            mockRequestGen.Verify(x => x.GenerateValidationRequest(bodyStr, model.Test_Ipn == 1), Times.Once);
            mockClientHelper.Verify(x => x.SendAsync(req), Times.Once);
        }

        #endregion

        #endregion
    }
}
