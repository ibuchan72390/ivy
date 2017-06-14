using Ivy.IoC;
using Ivy.Utility.Core.Extensions;
using System.Net.Http;
using Xunit;
using System.Text;
using System;
using Ivy.MailChimp.Test.Base;
using Ivy.MailChimp.Core.Services;
using Moq;
using Ivy.MailChimp.Core.Providers;
using Ivy.IoC.Core;
using Ivy.MailChimp.Core.Models;

namespace Ivy.MailChimp.Test
{
    public class MailChimpRequestFactoryTests : MailChimpTestBase
    {
        #region Variables & Constants

        private IMailChimpRequestFactory _sut;

        private const string testDataCenter = "TESTDC";
        private const string testListId = "TESTListId";
        private const string testApiKey = "TESTApiKey";

        #endregion

        #region SetUp & TearDown

        public MailChimpRequestFactoryTests()
        {
            var _mockConfigProvider = new Mock<IMailChimpConfigurationProvider>();

            _mockConfigProvider.Setup(x => x.DataCenter).Returns(testDataCenter);
            _mockConfigProvider.Setup(x => x.ListId).Returns(testListId);
            _mockConfigProvider.Setup(x => x.ApiKey).Returns(testApiKey);

            var containerGenerator = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGenerator);

            containerGenerator.RegisterInstance<IMailChimpConfigurationProvider>(_mockConfigProvider.Object);

            var container = containerGenerator.GenerateContainer();

            _sut = container.Resolve<IMailChimpRequestFactory>();
        }

        #endregion

        #region Tests

        #region GenerateGetMemberRequest

        [Fact]
        public void GenerateGetMemberRequest_Formats_Request_As_Expected()
        {
            const string testEmail = "test@gmail.com";

            var memberRequest = _sut.GenerateGetMemberRequest(testEmail);

            string expectedRef = $"lists/{testListId}/members/{testEmail.ToMD5Hash()}?fields=id,email_address,status";

            BaseAssert(memberRequest, HttpMethod.Get, GetExpectedUrl(expectedRef));
        }

        #endregion

        #region GenerateSubmitMemberRequest

        [Fact]
        public void GenerateSubmitMemberRequest_Formats_Request_As_Expected()
        {
            var submitRequest = new MailChimpContactInfo
            {
                email_address = "TEST@gmail.com",
                merge_fields = new MailChimpMergeFields { NAME = "TEST" }
            };

            var memberRequest = _sut.GenerateSubmitMemberRequest(submitRequest);

            string expectedRef = $"lists/{testListId}/members";

            BaseAssert(memberRequest, HttpMethod.Post, GetExpectedUrl(expectedRef));
        }

        #endregion

        #region GenerateEditMemberRequest

        [Fact]
        public void GenerateEditMemberRequest_Formats_Request_As_Expected()
        {
            var submitRequest = new MailChimpMember
            {
                email_address = "TEST@gmail.com",
                merge_fields = new MailChimpMergeFields { NAME = "TEST" }
            };

            var memberRequest = _sut.GenerateEditMemberRequest(submitRequest);

            string expectedRef = $"lists/{testListId}/members/{submitRequest.email_address.ToMD5Hash()}";

            BaseAssert(memberRequest, HttpMethod.Put, GetExpectedUrl(expectedRef));
        }

        #endregion

        #region GenerateGetListRequest

        [Fact]
        public void GenerateGetListRequest_Formats_Request_As_Expected()
        {
            var memberRequest = _sut.GenerateGetListRequest(testListId);

            string expectedRef = $"lists/{testListId}";

            BaseAssert(memberRequest, HttpMethod.Get, GetExpectedUrl(expectedRef));
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
