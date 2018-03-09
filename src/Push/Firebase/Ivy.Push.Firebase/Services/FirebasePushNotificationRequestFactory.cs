using Ivy.Google.Core.Interfaces.Services;
using Ivy.Push.Core.Interfaces.Models.Messages;
using Ivy.Push.Firebase.Core.Interfaces.Providers;
using Ivy.Push.Firebase.Core.Interfaces.Services;
using Ivy.Push.Firebase.Core.Models.Wrappers;
using Ivy.Web.Core.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ivy.Push.Firebase.Services
{
    public class FirebasePushNotificationRequestFactory :
        IFirebasePushNotificationFactory
    {
        #region Variables & Constants

        // Firebase is a google service provider, need to use their OAuth 2.0 Token
        private readonly IGoogleAccessTokenGenerator _googleTokenGenerator;

        private readonly IPushNotificationConfigurationProvider _configProvider;

        private readonly IJsonSerializationService _serializer;

        // Required scope for Firebase Messaging indicated here...
        // https://firebase.google.com/docs/cloud-messaging/auth-server
        private readonly string[] firebaseScopes = new string[1] 
        {
            "https://www.googleapis.com/auth/firebase.messaging"
        };

        #endregion

        #region Constructor

        public FirebasePushNotificationRequestFactory(
            IGoogleAccessTokenGenerator googleTokenGenerator,
            IPushNotificationConfigurationProvider configProvider,
            IJsonSerializationService serializer)
        {
            _googleTokenGenerator = googleTokenGenerator;
            _configProvider = configProvider;
            _serializer = serializer;
        }

        #endregion

        #region Public Methods

        public async Task<HttpRequestMessage> GeneratePushMessageRequestAsync(IDataPushMessage message)
        {
            var req = new HttpRequestMessage();

            req.Method = HttpMethod.Post;

            var authToken = await _googleTokenGenerator.GetOAuthTokenAsync(firebaseScopes);

            req.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer", authToken);

            req.RequestUri = new Uri(_configProvider.PushUrl);

            var submissionModel = new PushMessageWrapper { message = message };

            var json = _serializer.Serialize(submissionModel);

            req.Content = new StringContent(json, Encoding.Default, "application/json");

            return req;
        }

        #endregion
    }
}
