using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.TestHelper.TestEntities;
using Ivy.Web.Core.Client;
using Ivy.Web.Core.Json;
using Ivy.Web.Json;
using Ivy.Web.Test.Base;
using Moq;
using System;
using System.Net.Http;
using Xunit;

namespace Ivy.Web.Test.Client
{
    public class ApiHelperTests : WebTestBase<IApiHelper>
    {
        #region Variables & Constants

        private readonly HttpRequestMessage request;
        private readonly HttpResponseMessage response;

        private Mock<IHttpClientHelper> _clientHelperMock;
        private Mock<IJsonSerializationService> _serializerMock;

        #endregion

        #region SetUp & TearDown

        public ApiHelperTests()
        {
            request = new HttpRequestMessage { RequestUri = new Uri("http://google.com") };
            response = new HttpResponseMessage { RequestMessage = request };
        }

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _clientHelperMock = InitializeMoq<IHttpClientHelper>(containerGen);
            _serializerMock = InitializeMoq<IJsonSerializationService>(containerGen);
        }

        #endregion

        #region Tests

        #region GetApiData

        [Fact]
        public void GetApiData_Returns_As_Expected()
        {
            const string testContent = "TEST-Content";
            var expected = new BlobEntity();

            response.Content = new StringContent(testContent);

            _clientHelperMock.Setup(x => x.Send(request)).Returns(response);

            _serializerMock.Setup(x => x.Deserialize<BlobEntity>(testContent)).Returns(expected);

            var result = Sut.GetApiData<BlobEntity>(request);

            Assert.Same(expected, result);
        }

        [Fact]
        public void GetApiData_Throws_Error_If_Response_Not_Success()
        {
            response.StatusCode = System.Net.HttpStatusCode.NotFound;

            _clientHelperMock.Setup(x => x.Send(request)).Returns(response);

            var e = Assert.Throws<Exception>(() => Sut.GetApiData<BlobEntity>(request));

            string expectedErr = "Unsuccessful response received when requesting API! " +
                                 $"Request Uri: {response.RequestMessage.RequestUri.ToString()} / " +
                                 $"Status Code: {response.StatusCode.ToString()} / Reason: {response.ReasonPhrase} / " +
                                 $"Message: {response.RequestMessage}";


            Assert.Equal(expectedErr, e.Message);

            _clientHelperMock.Verify(x => x.Send(request), Times.Once);
        }

        #endregion

        #region SendApiData

        [Fact]
        public void SendApiData_Returns_As_Expected()
        {
            const string testContent = "TEST-Content";

            response.Content = new StringContent(testContent);

            _clientHelperMock.Setup(x => x.Send(request)).Returns(response);

            var result = Sut.SendApiData(request);

            Assert.Same(response, result);
        }

        [Fact]
        public void SendApiData_Throws_Error_If_Response_Not_Success()
        {
            response.StatusCode = System.Net.HttpStatusCode.NotFound;

            _clientHelperMock.Setup(x => x.Send(request)).Returns(response);

            var e = Assert.Throws<Exception>(() => Sut.SendApiData(request));

            string expectedErr = "Unsuccessful response received when requesting API! " +
                                 $"Request Uri: {response.RequestMessage.RequestUri.ToString()} / " +
                                 $"Status Code: {response.StatusCode.ToString()} / Reason: {response.ReasonPhrase} / " +
                                 $"Message: {response.RequestMessage}";


            Assert.Equal(expectedErr, e.Message);

            _clientHelperMock.Verify(x => x.Send(request), Times.Once);
        }

        #endregion

        #region GetApiDataAsync

        [Fact]
        public async void GetApiDataAsync_Returns_As_Expected()
        {
            const string testContent = "TEST-Content";
            var expected = new BlobEntity();

            response.Content = new StringContent(testContent);

            _clientHelperMock.Setup(x => x.SendAsync(request)).ReturnsAsync(response);

            _serializerMock.Setup(x => x.Deserialize<BlobEntity>(testContent)).Returns(expected);

            var result = await Sut.GetApiDataAsync<BlobEntity>(request);

            Assert.Same(expected, result);
        }

        [Fact]
        public async void GetApiDataAsync_Throws_Error_If_Response_Not_Success()
        {
            response.StatusCode = System.Net.HttpStatusCode.NotFound;

            _clientHelperMock.Setup(x => x.SendAsync(request)).ReturnsAsync(response);

            var e = await Assert.ThrowsAsync<Exception>(async () => await Sut.GetApiDataAsync<BlobEntity>(request));

            string expectedErr = "Unsuccessful response received when requesting API! " +
                                 $"Request Uri: {response.RequestMessage.RequestUri.ToString()} / " +
                                 $"Status Code: {response.StatusCode.ToString()} / Reason: {response.ReasonPhrase} / " +
                                 $"Message: {response.RequestMessage}";


            Assert.Equal(expectedErr, e.Message);

            _clientHelperMock.Verify(x => x.SendAsync(request), Times.Once);
        }

        #endregion

        #region SendApiDataAsync

        [Fact]
        public async void SendApiDataAsync_Returns_As_Expected()
        {
            const string testContent = "TEST-Content";

            response.Content = new StringContent(testContent);

            _clientHelperMock.Setup(x => x.SendAsync(request)).ReturnsAsync(response);

            var result = await Sut.SendApiDataAsync(request);

            Assert.Same(response, result);
        }

        [Fact]
        public async void SendApiDataAsync_Throws_Error_If_Response_Not_Success()
        {
            response.StatusCode = System.Net.HttpStatusCode.NotFound;

            _clientHelperMock.Setup(x => x.SendAsync(request)).ReturnsAsync(response);

            var e = await Assert.ThrowsAsync<Exception>(async () => await Sut.SendApiDataAsync(request));

            string expectedErr = "Unsuccessful response received when requesting API! " +
                                 $"Request Uri: {response.RequestMessage.RequestUri.ToString()} / " +
                                 $"Status Code: {response.StatusCode.ToString()} / Reason: {response.ReasonPhrase} / " +
                                 $"Message: {response.RequestMessage}";


            Assert.Equal(expectedErr, e.Message);

            _clientHelperMock.Verify(x => x.SendAsync(request), Times.Once);
        }

        #endregion

        #endregion
    }
}
