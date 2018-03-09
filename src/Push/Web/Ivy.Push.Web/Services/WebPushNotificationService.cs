using System.Threading.Tasks;
using Ivy.Push.Firebase.Core.Interfaces.Models.Messages;
using Ivy.Push.Web.Core.Interfaces.Providers;
using Ivy.Push.Web.Core.Interfaces.Services;
using Ivy.Web.Core.Json;

namespace Ivy.Push.Web.Services
{
    public class WebPushNotificationService : IWebPushNotificationService
    {
        #region Variables & Constants

        private readonly IWebPushClientService _clientService;

        private readonly IWebPushConfigurationProvider _config;

        private readonly IJsonSerializationService _jsonSerializer;

        #endregion

        #region Constructor

        public WebPushNotificationService(
            IWebPushClientService clientService,
            IWebPushConfigurationProvider config,
            IJsonSerializationService jsonSerializer)
        {
            _clientService = clientService;

            _config = config;

            _jsonSerializer = jsonSerializer;
        }

        #endregion

        #region Public Methods

        public void PushNotification(string endpoint, string p256dh, string auth, IDataPushMessage message)
        {
            var payload = _jsonSerializer.Serialize(message);

            _clientService.GetClient().SendNotification(endpoint, p256dh, auth, payload);
        }

        public async Task PushNotificationAsync(string endpoint, string p256dh, string auth, IDataPushMessage message)
        {
            var payload = _jsonSerializer.Serialize(message);

            await _clientService.GetClient().SendNotificationAsync(endpoint, p256dh, auth, payload);
        }

        #endregion
    }
}
