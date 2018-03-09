using System.Threading.Tasks;
using Ivy.Push.Core.Interfaces.Models.Messages;
using Ivy.Push.Firebase.Core.Interfaces.Services;
using Ivy.Push.Firebase.Core.Models.Responses;
using Ivy.Web.Core.Client;

namespace Ivy.Push.Firebase.Services
{
    public class FirebasePushNotificationService :
        IFirebasePushNotificationService
    {
        #region Variables & Constants

        private readonly IApiHelper _apiHelper;

        private readonly IFirebasePushNotificationFactory _requestFactory;

        #endregion

        #region Constructor

        public FirebasePushNotificationService(
            IApiHelper apiHelper,
            IFirebasePushNotificationFactory requestFactory)
        {
            _apiHelper = apiHelper;
            _requestFactory = requestFactory;
        }

        #endregion

        #region Public Methods

        public async Task<PushResponse> SendPushNotificationAsync(IDataPushMessage message)
        {
            var req = await _requestFactory.GeneratePushMessageRequestAsync(message);

            return await _apiHelper.GetApiDataAsync<PushResponse>(req);
        }

        #endregion
    }
}
