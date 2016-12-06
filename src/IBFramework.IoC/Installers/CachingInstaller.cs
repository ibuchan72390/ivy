//using IB.Framework.Core.Caching;
//using IB.Framework.Core.Enum;
//using IB.Framework.Core.IoC;
//using IB.Framework.Caching;

//namespace IB.Framework.IoC.Installers
//{
//    public class CachingInstaller : IContainerInstaller
//    {
//        public void Install(IContainerGenerator containerGenerator)
//        {
//            containerGenerator.Register<TriggerFileGenerator>().As<ITriggerFileGenerator>().WithLifestyle(RegistrationLifestyleType.Singleton);

//            containerGenerator.Register(typeof(ObjectCache<>)).As(typeof(IObjectCache<>)).WithLifestyle(RegistrationLifestyleType.Transient);
//        }
//    }

//    public static class CachingInstallerExtension
//    {
//        public static void InstallCaching(this IContainerGenerator containerGenerator)
//        {
//            new CachingInstaller().Install(containerGenerator);
//        }
//    }

//}
