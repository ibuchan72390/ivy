using Ivy.Caching.Core;
using Ivy.IoC.Core;

namespace Ivy.Caching.IoC
{
    public class CachingInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton<ITriggerFileManager, TriggerFileManager>();
            containerGenerator.RegisterSingleton<ICacheAccessor, CacheAccessor>();
            containerGenerator.RegisterTransient(typeof(IObjectCache<>), typeof(ObjectCache<>));
        }
    }

    public static class CachingInstallerExtension
    {
        public static IContainerGenerator InstallIvyCaching(this IContainerGenerator containerGenerator)
        {
            new CachingInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
