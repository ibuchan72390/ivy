using Ivy.Google.Core.Interfaces.Services;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Push.Core.Interfaces.Providers;
using Ivy.Push.Core.Interfaces.Services;
using Ivy.Push.Core.Models.Messages;
using Ivy.Push.Core.Models.Wrappers;
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
        FirebasePushMessageTestBase
    {
        #region Variables & Constants

        private readonly IPushNotificationRequestFactory _sut;

        private readonly Mock<IPushNotificationConfigurationProvider> _mockConfigProvider;
        private readonly Mock<IJsonSerializationService> _mockSerializer;
        private readonly Mock<IGoogleAccessTokenGenerator> _mockTokenGenerator;

        private const string scheme = "Bearer";
        private const string token = "Token";
        private const string url = "https://www.test.com/";

        const string firebaseScope = "https://www.googleapis.com/auth/firebase.messaging";

        #endregion

        #region SetUp & TearDown

        public FirebasePushNotificationRequestFactoryTests()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);
            
            _mockConfigProvider = new Mock<IPushNotificationConfigurationProvider>();
            _mockConfigProvider.Setup(x => x.PushUrl).Returns(url);
            containerGen.RegisterInstance<IPushNotificationConfigurationProvider>(_mockConfigProvider.Object);

            _mockSerializer = new Mock<IJsonSerializationService>();
            containerGen.RegisterInstance<IJsonSerializationService>(_mockSerializer.Object);

            _mockTokenGenerator = new Mock<IGoogleAccessTokenGenerator>();
            containerGen.RegisterInstance<IGoogleAccessTokenGenerator>(_mockTokenGenerator.Object);

            _mockTokenGenerator.Setup(x => x.GetOAuthTokenAsync(It.Is<string[]>(y => y.Length == 1 &&
                                                                                     y.Contains(firebaseScope)))).
                ReturnsAsync(token);

            _sut = containerGen.GenerateContainer().GetService<IPushNotificationRequestFactory>();
        }

        #endregion

        #region Tests

        #region GeneratePushMessageRequest (Device)
        
        [Fact]
        public async void GeneratePushMessageRequest_Generates_Correctly_For_Device()
        {
            // Arrange
            var msg = new DeviceNotificationPushMessage();

            await TestGeneratePushMessageRequestAsync(msg, async model => await _sut.GeneratePushMessageRequestAsync(model));
        }

        #endregion

        #region GeneratePushMessageRequest (Topic)

        [Fact]
        public async void GeneratePushMessageRequest_Generates_Correctly_For_Topic()
        {
            var msg = new TopicNotificationPushMessage();

            await TestGeneratePushMessageRequestAsync(msg, async model => await _sut.GeneratePushMessageRequestAsync(model));
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
