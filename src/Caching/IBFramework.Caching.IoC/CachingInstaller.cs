using IBFramework.Caching.Core;
using IBFramework.IoC.Core;

namespace IBFramework.Caching.IoC
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
        public static IContainerGenerator InstallCaching(this IContainerGenerator containerGenerator)
        {
            new CachingInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
