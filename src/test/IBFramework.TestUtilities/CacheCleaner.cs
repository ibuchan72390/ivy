using IBFramework.Core.Caching;
using System.IO;

namespace IBFramework.TestUtilities
{
    public static class CacheCleaner
    {
        public static void ResetCache<T>()
        {
            var fileManager = TestServiceLocator.StaticContainer.Resolve<ITriggerFileManager>();
            fileManager.TriggerCache<T>();
        }

        public static void ResetAllCache()
        {
            var fileManager = TestServiceLocator.StaticContainer.Resolve<ITriggerFileManager>();
            
            foreach (var file in Directory.GetFiles(fileManager.TriggerFileFolder))
            {
                File.Delete(file);
            }
        }
    }
}
