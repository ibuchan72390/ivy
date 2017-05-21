namespace IBFramework.Caching.Core
{
    public interface ITriggerFileManager
    {
        /// <summary>
        /// Returns the current configured TriggerFileFolder as set by the SetTriggerFileFolder method or determined
        /// as the default path.
        /// </summary>
        string TriggerFileFolder { get; }

        /// <summary>
        /// Sets the folder to maintain the trigger files.  If not set, simply creates a new "AppCache" folder
        /// in the root directory of the application.
        /// </summary>
        /// <param name="folderLocation"></param>
        void SetTriggerFileFolder(string folderLocation);

        /// <summary>
        /// Generates a cache trigger file.
        /// Currently only allows one cache per type.
        /// Implement the name key to allow for multiple caches per type
        /// </summary>
        /// <typeparam name="T">Entity being cached</typeparam>
        /// TODO: <param name="nameKey"> key to ensure your cache doesn't overwrite another cache of the same type</param>
        /// <returns></returns>
        // TODO: string GenerateTriggerFile<T>(string nameKey = null);
        string GenerateTriggerFile<T>(string cacheKey = null);

        /// <summary>
        /// Gets the path of the trigger file for the specified object type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        string GetTriggerFilePath<T>(string cacheKey = null);

        /// <summary>
        /// Returns a boolean indicating whether or not the cache should be refreshed based on the trigger file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool ShouldRefreshCache<T>(string cacheKey = null);

        /// <summary>
        /// Edits the trigger file to ensure that ShouldRefreshCache returns true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void TriggerCache<T>(string cacheKey = null);
    }
}
