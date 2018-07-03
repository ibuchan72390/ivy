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
    public class WebPushServiceTests : 
        BaseWebPushTest<IWebPushNotificationService>
    {
        #region Variables & Constants

        private Mock<IWebPushClientService> _mockClientService;
        private Mock<IWebPushConfigurationProvider> _mockConfigProvider;
        private Mock<IJsonSerializationService> _mockSerializer;
        private Mock<IWebPushClient> _mockClient;

        const string endpoint = "endpoint";
        const string p256dh = "p256dh";
        const string auth = "auth";
        private readonly IDataPushMessage msg = new DataPushMessage();
        const string serializedMsg = "Serialized Msg";

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockClientService = InitializeMoq<IWebPushClientService>(containerGen);
            _mockConfigProvider = InitializeMoq<IWebPushConfigurationProvider>(containerGen);
            _mockSerializer = InitializeMoq<IJsonSerializationService>(containerGen);

            _mockClient = InitializeMoq<IWebPushClient>(containerGen);
            _mockClientService.Setup(x => x.GetClient()).Returns(_mockClient.Object);
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
            Sut.PushNotification(endpoint, p256dh, auth, msg);


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
            await Sut.PushNotificationAsync(endpoint, p256dh, auth, msg);


            // Assert
            _mockSerializer.Verify(x => x.Serialize(msg), Times.Once);

            _mockClientService.Verify(x => x.GetClient(), Times.Once);

            _mockClient.Verify(x => x.SendNotificationAsync(endpoint, p256dh, auth, serializedMsg), Times.Once);
        }

        #endregion

        #endregion
    }
}
