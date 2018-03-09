using Ivy.Push.Web.Core.Interfaces.Clients;
using Ivy.Push.Web.Core.Interfaces.Providers;
using Ivy.Push.Web.Core.Interfaces.Services;
using WebPush;

namespace Ivy.Push.Web.Services
{
    public class WebPushClientService : IWebPushClientService
    {
        #region Variables & Constants

        private readonly IWebPushClient _client;

        #endregion

        #region Constructor

        public WebPushClientService(
            IWebPushClientGenerator clientGenerator,
            IWebPushConfigurationProvider config)
        {
            _client = clientGenerator.GenerateClient();

            // We'll configure the client with all of the appropriate configurations from the provider
            _client.SetVapidDetails(config.PushSubject, config.VapidPublicKey, config.VapidPrivateKey);
            _client.SetGcmApiKey(config.GcmApiKey);
        }

        #endregion

        #region Public Methods

        public IWebPushClient GetClient()
        {
            return _client;
        }

        #endregion
    }
}
