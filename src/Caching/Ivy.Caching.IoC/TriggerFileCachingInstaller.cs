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
            containerGenerator.RegisterTransient(typeof(IObjectCache<>), typeof(TriggerFileObjectCache<>));
            containerGenerator.RegisterTransient(typeof(ITriggerFileManager), typeof(TriggerFileManager));
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
