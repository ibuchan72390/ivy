using Ivy.Email.Common.Services;
using Ivy.Email.Core.Services;
using Ivy.IoC.Core;

namespace Ivy.Email.Common.IoC
{
    public class CommonEmailInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<ISendEmailModelValidator, SendEmailModelValidator>();
        }
    }

    public static class CommonEmailInstallerExtension
    {
        public static IContainerGenerator InstallIvyCommonEmail(this IContainerGenerator containerGenerator)
        {
            new CommonEmailInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
