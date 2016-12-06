namespace IBFramework.Core.Caching
{
    public interface ITriggerFileGenerator
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
        /// Generates a cache trigger file
        /// </summary>
        /// <typeparam name="T">Entity being cached</typeparam>
        /// <param name="nameKey">Additional key to ensure your cache doesn't overwrite another cache of the same type</param>
        /// <returns></returns>
        string GenerateTriggerFile<T>(string nameKey = null, string fileLocation = null);
    }
}
