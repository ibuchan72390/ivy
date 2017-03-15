using IBFramework.Core.Caching;
using IBFramework.Core.IoC;

namespace IBFramework.Caching.IoC
{
    public class CachingInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            //containerGenerator.Register<TriggerFileManager>().As<ITriggerFileManager>().WithLifestyle(RegistrationLifestyleType.Singleton);
            //containerGenerator.Register<CacheAccessor>().As<ICacheAccessor>().WithLifestyle(RegistrationLifestyleType.Singleton);
            //containerGenerator.Register(typeof(ObjectCache<>)).As(typeof(IObjectCache<>)).WithLifestyle(RegistrationLifestyleType.Transient);

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
