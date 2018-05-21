using System;
using System.Collections.Generic;

namespace Ivy.ReCaptcha.Core.Interfaces.Models
{
    public interface IReCaptchaResponse
    {
        bool Success { get; set; }

        DateTime ChallengeTimeStamp { get; set; }

        IList<string> ErrorCodes { get; set; }
    }
}
