using Ivy.Auth0.Core.Models.Requests;
using Ivy.Auth0.Core.Services;
using Ivy.Web.Core.Json;
using Ivy.Web.Json;
using Ivy.Auth0.Core.Models.Interfaces;

namespace Ivy.Auth0.Services
{
    public class Auth0JsonGenerator : IAuth0JsonGenerator
    {
        #region Variables & Constants

        private readonly IAuth0JsonManipulator _auth0Manipulator;
        private readonly IJsonManipulationService _jsonManipulator;
        private readonly IJsonSerializationService _serializationService;

        #endregion

        #region Constructor

        public Auth0JsonGenerator(
            IAuth0JsonManipulator auth0Manipulator,
            IJsonManipulationService jsonManipulator,
            IJsonSerializationService serializationService)
        {
            _auth0Manipulator = auth0Manipulator;
            _jsonManipulator = jsonManipulator;
            _serializationService = serializationService;
        }

        #endregion

        #region Public Methods

        public string ConfigureCreateUserJson(Auth0CreateUserRequest request)
        {
            return BaseConfigureJson(request);
        }

        public string ConfigureUpdateUserJson(Auth0UpdateUserRequest request)
        {
            var json = BaseConfigureJson(request);

            // Seems that we can't maintain this on the model, they expect this as a URL param
            json = _jsonManipulator.RemoveJsonAttribute(json, "user_id");

            if (string.IsNullOrEmpty(request.phone_number))
            {
                json = _jsonManipulator.RemoveJsonAttribute(json, "verify_phone_number");
            }

            if (string.IsNullOrEmpty(request.email))
            {
                json = _jsonManipulator.RemoveJsonAttribute(json, "email");
                json = _jsonManipulator.RemoveJsonAttribute(json, "email_verified");
                json = _jsonManipulator.RemoveJsonAttribute(json, "verify_email");
            }

            if (string.IsNullOrEmpty(request.password))
            {
                json = _jsonManipulator.RemoveJsonAttribute(json, "password");
                json = _jsonManipulator.RemoveJsonAttribute(json, "verify_password");
            }

            return json;
        }

        #endregion

        #region Helper Methods

        private string BaseConfigureJson<T>(T request)
            where T : IAuth0Phone, IAuth0Username
        {
            var json = _serializationService.Serialize(request);
            json = _auth0Manipulator.EditUsernameJson(json, request);
            return _auth0Manipulator.EditPhoneJson(json, request);
        }

        #endregion
    }
}
