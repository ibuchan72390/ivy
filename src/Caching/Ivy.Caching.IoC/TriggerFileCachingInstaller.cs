using Ivy.Caching.Core;
using Ivy.Caching.Core.TriggerFile;
using Ivy.Caching.TriggerFile;
using Ivy.IoC.Core;

namespace Ivy.Caching.IoC
{
    public class TriggerFileCachingInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.RegisterSingleton(typeof(IObjectCache<>), typeof(TriggerFileObjectCache<>));
            containerGenerator.RegisterSingleton(typeof(ITriggerFileManager), typeof(TriggerFileManager));
        }
    }

    public static class TriggerFileCachingInstallerExtension
    {
        public static IContainerGenerator InstallIvyTriggerFileCaching(this IContainerGenerator containerGenerator)
        {
            new TriggerFileCachingInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
