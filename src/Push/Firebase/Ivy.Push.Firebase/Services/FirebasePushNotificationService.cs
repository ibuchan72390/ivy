using System.Threading.Tasks;
using Ivy.Push.Core.Interfaces.Services;
using Ivy.Push.Core.Models.Messages;
using Ivy.Push.Core.Models.Responses;
using Ivy.Web.Core.Client;

namespace Ivy.Push.Firebase.Services
{
    public class FirebasePushNotificationService :
        IPushNotificationService
    {
        #region Variables & Constants

        private readonly IApiHelper _apiHelper;

        private readonly IPushNotificationRequestFactory _requestFactory;

        #endregion

        #region Constructor

        public FirebasePushNotificationService(
            IApiHelper apiHelper,
            IPushNotificationRequestFactory requestFactory)
        {
            _apiHelper = apiHelper;
            _requestFactory = requestFactory;
        }

        #endregion

        #region Public Methods

        public async Task<PushResponse> SendPushNotificationAsync(DevicePushMessage message)
        {
            var req = await _requestFactory.GeneratePushMessageRequestAsync(message);

            return await _apiHelper.GetApiDataAsync<PushResponse>(req);
        }

        public async Task<PushResponse> SendPushNotificationAsync(TopicPushMessage message)
        {
            var req = await _requestFactory.GeneratePushMessageRequestAsync(message);

            return await _apiHelper.GetApiDataAsync<PushResponse>(req);
        }

        #endregion
    }
}
