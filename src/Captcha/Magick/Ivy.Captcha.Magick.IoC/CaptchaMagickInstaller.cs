using Ivy.Captcha.Core.Interfaces.Services;
using Ivy.Captcha.Magick.Services;
using Ivy.IoC.Core;

namespace Ivy.Captcha.Magick.IoC
{
    public class CaptchaInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<ICaptchaGenerationService, MagickCaptchaGenerationService>();
        }
    }

    public static class CaptchaInstallerExtension
    {
        public static IContainerGenerator InstallIvyCaptchaMagick(this IContainerGenerator containerGenerator)
        {
            new CaptchaInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
