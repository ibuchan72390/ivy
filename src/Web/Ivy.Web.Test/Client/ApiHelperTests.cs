using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.TestHelper.TestEntities;
using Ivy.Web.Core.Client;
using Ivy.Web.Json;
using Ivy.Web.Test.Base;
using Moq;
using System;
using System.Net.Http;
using Xunit;

namespace Ivy.Web.Test.Client
{
    public class ApiHelperTests : WebTestBase
    {
        #region Variables & Constants

        private readonly HttpRequestMessage request;
        private readonly HttpResponseMessage response;

        private readonly IApiHelper _sut;

        private readonly Mock<IHttpClientHelper> _clientHelperMock;
        private readonly Mock<IJsonSerializationService> _serializerMock;

        #endregion

        #region SetUp & TearDown

        public ApiHelperTests()
        {
            request = new HttpRequestMessage { RequestUri = new Uri("http://google.com") };
            response = new HttpResponseMessage { RequestMessage = request };

            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _clientHelperMock = new Mock<IHttpClientHelper>();
            containerGen.RegisterInstance<IHttpClientHelper>(_clientHelperMock.Object);

            _serializerMock = new Mock<IJsonSerializationService>();
            containerGen.RegisterInstance<IJsonSerializationService>(_serializerMock.Object);

            var container = containerGen.GenerateContainer();

            _sut = container.Resolve<IApiHelper>();
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

            var result = _sut.GetApiData<BlobEntity>(request);

            Assert.Same(expected, result);
        }

        [Fact]
        public void GetApiData_Throws_Error_If_Response_Not_Success()
        {
            response.StatusCode = System.Net.HttpStatusCode.NotFound;

            _clientHelperMock.Setup(x => x.Send(request)).Returns(response);

            var e = Assert.Throws<Exception>(() => _sut.GetApiData<BlobEntity>(request));

            string expectedErr = "Unsuccessful response received when requesting API! " +
                                 $"Request Uri: {response.RequestMessage.RequestUri.ToString()} / " +
                                 $"Status Code: {response.StatusCode} / Reason: {response.ReasonPhrase}";


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

            var result = _sut.SendApiData(request);

            Assert.Same(response, result);
        }

        [Fact]
        public void SendApiData_Throws_Error_If_Response_Not_Success()
        {
            response.StatusCode = System.Net.HttpStatusCode.NotFound;

            _clientHelperMock.Setup(x => x.Send(request)).Returns(response);

            var e = Assert.Throws<Exception>(() => _sut.SendApiData(request));

            string expectedErr = "Unsuccessful response received when requesting API! " +
                                 $"Request Uri: {response.RequestMessage.RequestUri.ToString()} / " +
                                 $"Status Code: {response.StatusCode} / Reason: {response.ReasonPhrase}";


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

            var result = await _sut.GetApiDataAsync<BlobEntity>(request);

            Assert.Same(expected, result);
        }

        [Fact]
        public async void GetApiDataAsync_Throws_Error_If_Response_Not_Success()
        {
            response.StatusCode = System.Net.HttpStatusCode.NotFound;

            _clientHelperMock.Setup(x => x.SendAsync(request)).ReturnsAsync(response);

            var e = await Assert.ThrowsAsync<Exception>(async () => await _sut.GetApiDataAsync<BlobEntity>(request));

            string expectedErr = "Unsuccessful response received when requesting API! " +
                                 $"Request Uri: {response.RequestMessage.RequestUri.ToString()} / " +
                                 $"Status Code: {response.StatusCode} / Reason: {response.ReasonPhrase}";


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

            var result = await _sut.SendApiDataAsync(request);

            Assert.Same(response, result);
        }

        [Fact]
        public async void SendApiDataAsync_Throws_Error_If_Response_Not_Success()
        {
            response.StatusCode = System.Net.HttpStatusCode.NotFound;

            _clientHelperMock.Setup(x => x.SendAsync(request)).ReturnsAsync(response);

            var e = await Assert.ThrowsAsync<Exception>(async () => await _sut.SendApiDataAsync(request));

            string expectedErr = "Unsuccessful response received when requesting API! " +
                                 $"Request Uri: {response.RequestMessage.RequestUri.ToString()} / " +
                                 $"Status Code: {response.StatusCode} / Reason: {response.ReasonPhrase}";


            Assert.Equal(expectedErr, e.Message);

            _clientHelperMock.Verify(x => x.SendAsync(request), Times.Once);
        }

        #endregion

        #endregion
    }
}
