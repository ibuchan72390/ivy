using Ivy.Auth0.Core.Models.Interfaces;
using Ivy.Auth0.Core.Providers;
using Ivy.Auth0.Core.Services;
using Ivy.Web.Core.Json;
using System;
using System.Text.RegularExpressions;

namespace Ivy.Auth0.Services
{
    public class Auth0JsonManipulator : IAuth0JsonManipulator
    {
        #region Variables & Constants

        private readonly IJsonManipulationService _jsonManipulator;
        private readonly IAuth0ConfigurationProvider _config;

        #endregion

        #region Constructor

        public Auth0JsonManipulator(
            IJsonManipulationService jsonManipulator,
            IAuth0ConfigurationProvider config)
        {
            _jsonManipulator = jsonManipulator;
            _config = config;
        }

        #endregion

        #region Public Methods

        public string EditPhoneJson(string json, IAuth0Phone request)
        {
            // If Phone is null or empty, we need to simply zero it out of the JSON
            if (request.phone_number == null || request.phone_number == "")
            {
                json = _jsonManipulator.RemoveJsonAttribute(json, "phone_number");
                json = _jsonManipulator.RemoveJsonAttribute(json, "phone_verified");
            }
            else
            {
                // If we save a phone number, it 100% MUST match this regex: ^\\+[0-9]{1,15}$
                // If this regex does not match, then we will definitely fail
                Match validPhone = Regex.Match(request.phone_number, "^\\+[0-9]{1,15}$");

                if (!validPhone.Success)
                {
                    throw new Exception("Invalid phone number received! Must match regex - ^\\+[0-9]{1,15}$" +
                        $" / Phone: {request.phone_number}");
                }
            }

            return json;
        }

        public string EditUsernameJson(string json, IAuth0Username model)
        {
            if (!_config.UseUsername || string.IsNullOrEmpty(model.username))
            {
                json = _jsonManipulator.RemoveJsonAttribute(json, "username");
            }

            return json;
        }

        #endregion
    }
}
