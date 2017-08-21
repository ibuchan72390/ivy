using Ivy.Email.Core.Services;
using Ivy.Email.SparkPost.Core.Services;
using Ivy.Email.SparkPost.Services;
using Ivy.IoC.Core;

namespace Ivy.Email.SparkPost.IoC
{
    public class SparkPostEmailInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IEmailTransmissionService, SparkPostEmailTransmissionService>();
            containerGenerator.RegisterSingleton<ISparkPostTransmissionGenerator, SparkPostTransmissionGenerator>();
        }
    }

    public static class SparkPostEmailInstallerExtension
    {
        public static IContainerGenerator InstallIvySparkPostEmail(this IContainerGenerator containerGenerator)
        {
            new SparkPostEmailInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
