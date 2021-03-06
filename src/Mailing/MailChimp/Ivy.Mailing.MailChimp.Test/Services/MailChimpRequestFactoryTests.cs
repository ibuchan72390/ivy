﻿using Ivy.IoC;
using Ivy.Utility.Core.Extensions;
using System.Net.Http;
using Xunit;
using System.Text;
using System;
using Ivy.Mailing.MailChimp.Test.Base;
using Moq;
using Ivy.Mailing.MailChimp.Core.Providers;
using Ivy.IoC.Core;
using Ivy.Mailing.MailChimp.Core.Models;
using Ivy.Mailing.Core.Interfaces.Services;
using Ivy.Mailing.Core.Models;
using System.Collections.Generic;
using Ivy.Mailing.MailChimp.Core.Interfaces.Transformers;
using Newtonsoft.Json;

namespace Ivy.Mailing.MailChimp.Test
{
    public class MailChimpRequestFactoryTests : 
        MailChimpTestBase<IMailingRequestFactory>
    {
        #region Variables & Constants

        private const string testDataCenter = "TESTDC";
        private const string testListId = "TESTListId";
        private const string testApiKey = "TESTApiKey";

        private Mock<IMailChimpContactTransformer> _mockTransformer;

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            var _mockConfigProvider = InitializeMoq<IMailChimpConfigurationProvider>(containerGen);
            _mockConfigProvider.Setup(x => x.DataCenter).Returns(testDataCenter);
            _mockConfigProvider.Setup(x => x.ListId).Returns(testListId);
            _mockConfigProvider.Setup(x => x.ApiKey).Returns(testApiKey);

            _mockTransformer = InitializeMoq<IMailChimpContactTransformer>(containerGen);
        }

        #endregion

        #region Tests

        #region GenerateGetMemberRequest

        [Fact]
        public void GenerateGetMemberRequest_Formats_Request_As_Expected()
        {
            const string testEmail = "test@gmail.com";

            var memberRequest = Sut.GenerateGetMemberRequest(testEmail);

            string expectedRef = $"lists/{testListId}/members/{testEmail.ToMD5Hash()}?fields=id,email_address,status";

            BaseAssert(memberRequest, HttpMethod.Get, GetExpectedUrl(expectedRef));
        }

        #endregion

        #region GenerateAddMemberRequest

        [Fact]
        public async void GenerateSubmitMemberRequest_Formats_Request_As_Expected()
        {
            var submitRequest = new MailingMember
            {
                Email = "TEST@gmail.com",

                ExtraData = new Dictionary<string, string>
                {
                    { "NAME", "TEST" }
                }
            };

            var resultMember = new MailChimpContactInfo { email_address = submitRequest.Email };

            _mockTransformer.Setup(x => x.Transform(submitRequest)).Returns(resultMember);

            var memberRequest = Sut.GenerateAddMemberRequest(submitRequest);

            string expectedRef = $"lists/{testListId}/members";

            BaseAssert(memberRequest, HttpMethod.Post, GetExpectedUrl(expectedRef));

            _mockTransformer.Verify(x => x.Transform(submitRequest), Times.Once);

            var resultContent = await memberRequest.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<MailChimpMember>(resultContent);

            Assert.Equal(resultMember.email_address, result.email_address);
        }

        #endregion

        #region GenerateEditMemberRequest

        [Fact]
        public async void GenerateEditMemberRequest_Formats_Request_As_Expected()
        {
            var submitRequest = new MailingMember
            {
                Email = "TEST@gmail.com",

                ExtraData = new Dictionary<string, string>
                {
                    { "NAME", "TEST" }
                }
            };

            var resultMember = new MailChimpContactInfo { email_address = submitRequest.Email };

            _mockTransformer.Setup(x => x.Transform(submitRequest)).Returns(resultMember);

            var memberRequest = Sut.GenerateEditMemberRequest(submitRequest);

            string expectedRef = $"lists/{testListId}/members/{submitRequest.Email.ToMD5Hash()}";

            BaseAssert(memberRequest, HttpMethod.Put, GetExpectedUrl(expectedRef));

            _mockTransformer.Verify(x => x.Transform(submitRequest), Times.Once);

            var resultContent = await memberRequest.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<MailChimpMember>(resultContent);

            Assert.Equal(resultMember.email_address, result.email_address);
        }

        #endregion

        #endregion

        #region Helper Methods

        private void BaseAssert(HttpRequestMessage request, HttpMethod expectedMethod, string expectedUrl)
        {
            var expectedHeader = $"anything:{testApiKey}";
            var headerBytes = Encoding.ASCII.GetBytes(expectedHeader);
            var headerVal = Convert.ToBase64String(headerBytes);

            Assert.Equal(expectedMethod, request.Method);
            Assert.Equal("Basic", request.Headers.Authorization.Scheme);
            Assert.Equal(headerVal, request.Headers.Authorization.Parameter);
            Assert.Equal(new Uri(expectedUrl).ToString(), request.RequestUri.AbsoluteUri.ToString());
        }

        private string GetExpectedUrl(string refPath)
        {
            return $"https://{testDataCenter}.api.mailchimp.com/3.0/" + refPath;
        }

        #endregion
    }
}
