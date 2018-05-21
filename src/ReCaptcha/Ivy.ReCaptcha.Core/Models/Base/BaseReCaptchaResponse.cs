using Ivy.ReCaptcha.Core.Interfaces.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Ivy.ReCaptcha.Core.Models.Base
{
    public abstract class BaseReCaptchaResponse : IReCaptchaResponse
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "challenge_ts")]
        public DateTime ChallengeTimeStamp { get; set; }

        [JsonProperty(PropertyName = "error-codes")]
        public IList<string> ErrorCodes { get; set; }
    }
}
