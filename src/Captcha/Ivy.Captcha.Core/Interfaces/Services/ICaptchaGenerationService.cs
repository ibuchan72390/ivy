using Ivy.Captcha.Core.Models;

namespace Ivy.Captcha.Core.Interfaces.Services
{
    public interface ICaptchaGenerationService
    {
        CaptchaResult GenerateCaptchaImage(int captchaCharLength, int width, int height);
    }
}
