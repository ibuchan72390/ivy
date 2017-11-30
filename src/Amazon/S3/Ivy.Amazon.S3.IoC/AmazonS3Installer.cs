using Ivy.Amazon.S3.Core.Services;
using Ivy.Amazon.S3.Services;
using Ivy.IoC.Core;

namespace Ivy.Amazon.S3.IoC
{
    public class AmazonS3Installer : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<IS3SignedUrlService, S3SignedUrlService>();
            containerGenerator.RegisterSingleton<IS3UrlHelper, S3UrlHelper>();
            containerGenerator.RegisterSingleton<IS3FileManipulator, S3FileManipulator>();
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
