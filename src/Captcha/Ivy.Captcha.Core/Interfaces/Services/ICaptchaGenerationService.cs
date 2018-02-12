using Ivy.Captcha.Core.Interfaces.Models;

namespace Ivy.Captcha.Core.Interfaces.Services
{
    public interface ICaptchaGenerationService
    {
        ICaptchaResult GenerateCaptchaImage(int captchaCharLength, int width, int height);
    }
}
