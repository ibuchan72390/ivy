using Ivy.Caching.Basic;
using Ivy.Caching.Core;
using Ivy.IoC.Core;

namespace Ivy.Caching.IoC
{
    public class BasicCachingInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton(typeof(IObjectCache<>), typeof(BasicObjectCache<>));
        }
    }

    public static class BasicCachingInstallerExtension
    {
        public static IContainerGenerator InstallIvyBasicCaching(this IContainerGenerator containerGenerator)
        {
            new BasicCachingInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
