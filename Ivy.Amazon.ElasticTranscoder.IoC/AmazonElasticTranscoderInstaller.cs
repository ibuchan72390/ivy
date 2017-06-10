using Ivy.Amazon.ElasticTranscoder.Core.Services;
using Ivy.Amazon.ElasticTranscoder.Services;
using Ivy.IoC.Core;

namespace Ivy.Amazon.ElasticTranscoder.IoC
{
    public class AmazonElasticTranscoderInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<ITranscoderStatusService, TranscoderStatusService>();
            containerGenerator.RegisterSingleton<ITranscoderWrapperService, TranscoderWrapperService>();
        }
    }

    public static class AmazonElasticTranscoderInstallerExtension
    {
        public static IContainerGenerator InstallIvyAmazonS3(this IContainerGenerator containerGenerator)
        {
            new AmazonElasticTranscoderInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
