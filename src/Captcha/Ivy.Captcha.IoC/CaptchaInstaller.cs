using Ivy.Captcha.Core.Interfaces.Services;
using Ivy.Captcha.Services;
using Ivy.IoC.Core;

namespace Ivy.Captcha.IoC
{
    public class CaptchaInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<ICaptchaCodeGenerator, CaptchaCodeGenerator>();
            containerGenerator.RegisterSingleton<ICaptchaDrawingService, CaptchaDrawingService>();
            containerGenerator.RegisterSingleton<ICaptchaGenerationService, CaptchaGenerationService>();
            containerGenerator.RegisterSingleton<ICaptchaImageHelper, CaptchaImageHelper>();
        }
    }

    public static class CaptchaInstallerExtension
    {
        public static IContainerGenerator InstallIvyCaptcha(this IContainerGenerator containerGenerator)
        {
            new CaptchaInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
