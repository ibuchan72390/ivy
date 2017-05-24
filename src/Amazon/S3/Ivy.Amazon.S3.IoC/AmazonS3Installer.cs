using Ivy.Amazon.S3.Core.Services;
using Ivy.Amazon.S3.Services;
using Ivy.IoC.Core;

namespace Ivy.Amazon.S3.IoC
{
    public class AmazonS3Installer : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IS3FileService, S3FileService>();
            containerGenerator.RegisterSingleton<IS3ResolutionTranslator, S3ResolutionTranslator>();
            containerGenerator.RegisterSingleton<IS3UrlHelper, S3UrlHelper>();
            containerGenerator.RegisterSingleton<IS3VideoKeyGenerator, S3VideoKeyGenerator>();
        }
    }

    public static class AmazonS3InstallerExtension
    {
        public static IContainerGenerator InstallIvyAmazonS3(this IContainerGenerator containerGenerator)
        {
            new AmazonS3Installer().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
