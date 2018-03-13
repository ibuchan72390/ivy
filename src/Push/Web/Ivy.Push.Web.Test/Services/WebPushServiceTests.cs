using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Push.Core.Interfaces.Models.Messages;
using Ivy.Push.Core.Models.Messages;
using Ivy.Push.Web.Core.Interfaces.Clients;
using Ivy.Push.Web.Core.Interfaces.Providers;
using Ivy.Push.Web.Core.Interfaces.Services;
using Ivy.Push.Web.Test.Base;
using Ivy.Web.Core.Json;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Ivy.Push.Web.Test.Services
{
    public class WebPushServiceTests : BaseWebPushTest
    {
        #region Variables & Constants

        private readonly IWebPushNotificationService _sut;

        private readonly Mock<IWebPushClientService> _mockClientService;
        private readonly Mock<IWebPushConfigurationProvider> _mockConfigProvider;
        private readonly Mock<IJsonSerializationService> _mockSerializer;
        private readonly Mock<IWebPushClient> _mockClient;

        const string endpoint = "endpoint";
        const string p256dh = "p256dh";
        const string auth = "auth";
        private readonly IDataPushMessage msg = new DataPushMessage();
        const string serializedMsg = "Serialized Msg";

        #endregion

        #region SetUp & TearDown

        public WebPushServiceTests()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockClientService = new Mock<IWebPushClientService>();
            containerGen.RegisterInstance<IWebPushClientService>(_mockClientService.Object);

            _mockConfigProvider = new Mock<IWebPushConfigurationProvider>();
            containerGen.RegisterInstance<IWebPushConfigurationProvider>(_mockConfigProvider.Object);

            _mockSerializer = new Mock<IJsonSerializationService>();
            containerGen.RegisterInstance<IJsonSerializationService>(_mockSerializer.Object);

            _mockClient = new Mock<IWebPushClient>();
            _mockClientService.Setup(x => x.GetClient()).Returns(_mockClient.Object);

            _sut = containerGen.GenerateContainer().GetService<IWebPushNotificationService>();
        }

        #endregion

        #region Tests

        #region PushNotification

        [Fact]
        public void PushNotification_Executes_As_Expected()
        {
            // Arrange
            _mockSerializer.Setup(x => x.Serialize(msg)).Returns(serializedMsg);

            _mockClient.Setup(x => x.SendNotification(endpoint, p256dh, auth, serializedMsg));


            // Act
            _sut.PushNotification(endpoint, p256dh, auth, msg);


            // Assert
            _mockSerializer.Verify(x => x.Serialize(msg), Times.Once);

            _mockClientService.Verify(x => x.GetClient(), Times.Once);

            _mockClient.Verify(x => x.SendNotification(endpoint, p256dh, auth, serializedMsg), Times.Once);
        }

        #endregion

        #region PushNotificationAsync

        [Fact]
        public async void PushNotificationAsync_Executes_As_Expected()
        {
            // Arrange
            _mockSerializer.Setup(x => x.Serialize(msg)).Returns(serializedMsg);

            _mockClient.
                Setup(x => x.SendNotificationAsync(endpoint, p256dh, auth, serializedMsg)).
                Returns(Task.FromResult(0));


            // Act
            await _sut.PushNotificationAsync(endpoint, p256dh, auth, msg);


            // Assert
            _mockSerializer.Verify(x => x.Serialize(msg), Times.Once);

            _mockClientService.Verify(x => x.GetClient(), Times.Once);

            _mockClient.Verify(x => x.SendNotificationAsync(endpoint, p256dh, auth, serializedMsg), Times.Once);
        }

        #endregion

        #endregion
    }
}
