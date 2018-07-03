using Ivy.Google.Core.Interfaces.Services;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Push.Core.Models.Messages;
using Ivy.Push.Firebase.Core.Interfaces.Providers;
using Ivy.Push.Firebase.Core.Interfaces.Services;
using Ivy.Push.Firebase.Core.Models.Wrappers;
using Ivy.Push.Firebase.Test.Base;
using Ivy.Web.Core.Json;
using Moq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Ivy.Push.Firebase.Test.Services
{
    public class FirebasePushNotificationRequestFactoryTests :
        FirebasePushMessageTestBase<IFirebasePushNotificationFactory>
    {
        #region Variables & Constants

        private Mock<IPushNotificationConfigurationProvider> _mockConfigProvider;
        private Mock<IJsonSerializationService> _mockSerializer;
        private Mock<IGoogleAccessTokenGenerator> _mockTokenGenerator;

        private const string scheme = "Bearer";
        private const string token = "Token";
        private const string url = "https://www.test.com/";

        const string firebaseScope = "https://www.googleapis.com/auth/firebase.messaging";

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockConfigProvider = InitializeMoq<IPushNotificationConfigurationProvider>(containerGen);
            _mockConfigProvider.Setup(x => x.PushUrl).Returns(url);

            _mockSerializer = InitializeMoq<IJsonSerializationService>(containerGen);

            _mockTokenGenerator = InitializeMoq<IGoogleAccessTokenGenerator>(containerGen);
            _mockTokenGenerator.Setup(x => x.GetOAuthTokenAsync(It.Is<string[]>(y => y.Length == 1 &&
                                                                                     y.Contains(firebaseScope)))).
                ReturnsAsync(token);
        }

        #endregion

        #region Tests

        #region GeneratePushMessageRequest (Device)

        [Fact]
        public async void GeneratePushMessageRequest_Generates_Correctly_For_Device()
        {
            // Arrange
            var msg = new NotificationPushMessage();

            await TestGeneratePushMessageRequestAsync(msg, async model => await Sut.GeneratePushMessageRequestAsync(model));
        }

        #endregion

        #endregion

        #region Helper Methods

        private async Task TestGeneratePushMessageRequestAsync<T>(T model, Func<T, Task<HttpRequestMessage>> getReqFn)
        {
            const string resultJson = "json";

            _mockSerializer.Setup(x => x.Serialize(It.Is<PushMessageWrapper>(y => y.message.Equals(model)))).Returns(resultJson);

            // Act
            var req = await getReqFn(model);

            // Assert
            Assert.Equal(url, req.RequestUri.ToString());
            Assert.Equal(HttpMethod.Post, req.Method);

            Assert.Equal(scheme, req.Headers.Authorization.Scheme);
            Assert.Equal(token, req.Headers.Authorization.Parameter);

            Assert.Equal("application/json", req.Content.Headers.ContentType.MediaType.ToString());

            var content = await req.Content.ReadAsStringAsync();

            Assert.Equal(resultJson, content);

            _mockSerializer.Verify(x => x.Serialize(It.Is<PushMessageWrapper>(y => y.message.Equals(model))), Times.Once);

            _mockTokenGenerator.Verify(x => x.GetOAuthTokenAsync(It.Is<string[]>(y => y.Length == 1 &&
                                                                                     y.Contains(firebaseScope))), 
                Times.Once);
        }

        #endregion
    }
}
