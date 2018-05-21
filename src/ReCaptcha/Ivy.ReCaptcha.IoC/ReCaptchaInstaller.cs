using Ivy.IoC.Core;
using Ivy.ReCaptcha.Core.Interfaces.Services;
using Ivy.ReCaptcha.Services;

/*
 * We should be able to make it so we can request a single base transformer if necessary
 * Seems that this pattern was pretty effective when working with Enum models.
 */

namespace Ivy.ReCaptcha.IoC
{
    public class ReCaptchaInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator container)
        {
            container.RegisterSingleton<IReCaptchaValidator, ReCaptchaValidator>();
            container.RegisterSingleton<IReCaptchaRequestGenerator, ReCaptchaRequestGenerator>();
            container.RegisterSingleton<IReCaptchaRequestContentGenerator, ReCaptchaRequestContentGenerator>();
        }
    }

    public static class TransformerInstallerExtension
    {
        public static IContainerGenerator InstallIvyReCaptcha(this IContainerGenerator containerGenerator)
        {
            new ReCaptchaInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
