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
    public class WebPushClientServiceTests : BaseWebPushTest
    {
        #region Variables & Constants

        private readonly IWebPushClientService _sut;

        private readonly Mock<IWebPushConfigurationProvider> _mockConfigProvider;
        private readonly Mock<IWebPushClientGenerator> _mockGenerator;
        private readonly Mock<IWebPushClient> _mockClient;

        private readonly string pushSubject = "mailto:test@gmail.com";
        private readonly string vapidPrivateKey = "VapidPrivate";
        private readonly string vapidPublicKey = "VapidPublic";
        private readonly string gcmApiKey = "GCMKey";

        #endregion

        #region SetUp & TearDown

        public WebPushClientServiceTests()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockConfigProvider = new Mock<IWebPushConfigurationProvider>();
            containerGen.RegisterInstance<IWebPushConfigurationProvider>(_mockConfigProvider.Object);

            _mockConfigProvider.Setup(x => x.PushSubject).Returns(pushSubject);
            _mockConfigProvider.Setup(x => x.VapidPrivateKey).Returns(vapidPrivateKey);
            _mockConfigProvider.Setup(x => x.VapidPublicKey).Returns(vapidPublicKey);
            _mockConfigProvider.Setup(x => x.GcmApiKey).Returns(gcmApiKey);

            _mockGenerator = new Mock<IWebPushClientGenerator>();
            containerGen.RegisterInstance<IWebPushClientGenerator>(_mockGenerator.Object);

            _mockClient = new Mock<IWebPushClient>();
            _mockGenerator.Setup(x => x.GenerateClient()).Returns(_mockClient.Object);

            _mockClient.Setup(x => x.SetVapidDetails(pushSubject, vapidPublicKey, vapidPrivateKey));
            _mockClient.Setup(x => x.SetGcmApiKey(gcmApiKey));

            _sut = containerGen.GenerateContainer().GetService<IWebPushClientService>();
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
            var created = new List<IWebPushClient> { _sut.GetClient() };

            for (var i = 0; i < 5; i++)
            {
                var item = _sut.GetClient();

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
