using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Push.Web.Core.Interfaces.Clients;
using Ivy.Push.Web.Core.Interfaces.Providers;
using Ivy.Push.Web.Core.Interfaces.Services;
using Ivy.Push.Web.Test.Base;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Ivy.Push.Web.Test.Services
{
    public class WebPushClientServiceTests : 
        BaseWebPushTest<IWebPushClientService>
    {
        #region Variables & Constants

        private Mock<IWebPushConfigurationProvider> _mockConfigProvider;
        private Mock<IWebPushClientGenerator> _mockGenerator;
        private Mock<IWebPushClient> _mockClient;

        private readonly string pushSubject = "mailto:test@gmail.com";
        private readonly string vapidPrivateKey = "VapidPrivate";
        private readonly string vapidPublicKey = "VapidPublic";
        private readonly string gcmApiKey = "GCMKey";

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockConfigProvider = InitializeMoq<IWebPushConfigurationProvider>(containerGen);
            _mockConfigProvider.Setup(x => x.PushSubject).Returns(pushSubject);
            _mockConfigProvider.Setup(x => x.VapidPrivateKey).Returns(vapidPrivateKey);
            _mockConfigProvider.Setup(x => x.VapidPublicKey).Returns(vapidPublicKey);
            _mockConfigProvider.Setup(x => x.GcmApiKey).Returns(gcmApiKey);

            _mockClient = InitializeMoq<IWebPushClient>(containerGen);
            _mockClient.Setup(x => x.SetVapidDetails(pushSubject, vapidPublicKey, vapidPrivateKey));
            _mockClient.Setup(x => x.SetGcmApiKey(gcmApiKey));

            _mockGenerator = InitializeMoq<IWebPushClientGenerator>(containerGen);
            _mockGenerator.Setup(x => x.GenerateClient()).Returns(_mockClient.Object);
        }

        #endregion

        #region Tests

        [Fact]
        public void WebPushClientService_Configures_As_Expected()
        {
            // Client should be initialized prior to any requests
            _mockClient.Verify(x => x.SetVapidDetails(pushSubject, vapidPublicKey, vapidPrivateKey), Times.Once);
            _mockClient.Verify(x => x.SetGcmApiKey(gcmApiKey), Times.Once);


            // We should get the same client every time we call this thing
            var created = new List<IWebPushClient> { Sut.GetClient() };

            for (var i = 0; i < 5; i++)
            {
                var item = Sut.GetClient();

                foreach (var validationItem in created)
                {
                    Assert.Same(item, validationItem);
                }

                created.Add(item);
            }
        }

        #endregion
    }
}
