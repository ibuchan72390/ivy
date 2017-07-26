using Ivy.Caching.Core.TriggerFile;
using System;
using System.Collections.Generic;
using System.IO;

namespace Ivy.Caching.TriggerFile
{
    /*
     * The trigger file manager will turn over control of the caching mechanism to the developers...
     * This way, the logic for caching isn't completely embedded in the startup functionality.
     * 
     * We can set sliding expirations in the startup; however, if we wish to manually refresh the
     * cache, we need to go in and restart the application.  With this process, you simply need to
     * delete the representative cache file and the cache will refresh on next hit.
     */
    public class TriggerFileManager : ITriggerFileManager
    {
        #region Variables & Constants

        const string defaultRootPath = "AppCache";

        private string _triggerFileFolder = null;

        private readonly string _defaultTriggerFileFolder;

        public string TriggerFileFolder => _triggerFileFolder;

        private Dictionary<string, DateTime> _cacheRegistration = new Dictionary<string, DateTime>();

        #endregion

        #region Constructor

        public TriggerFileManager()
        {
            var appRoot = Directory.GetCurrentDirectory();
            _defaultTriggerFileFolder = Path.Combine(appRoot, defaultRootPath);
        }

        #endregion

        #region Public Methods

        public string GenerateTriggerFile<T>(string cacheKey)
        {
            SetTriggerFileDefaultIfNecessary();

            if (!Directory.Exists(_triggerFileFolder))
            {
                Directory.CreateDirectory(_triggerFileFolder);
            }

            var fileToCreate = GetTriggerFilePath<T>(cacheKey);

            if (!File.Exists(fileToCreate))
            {
                var stream = new FileStream(fileToCreate, FileMode.Create, FileAccess.Write);
                stream.Dispose();
            }

            AddToCreatedHashes<T>(cacheKey);

            return fileToCreate;
        }

        public void SetTriggerFileFolder(string folderLocation)
        {
            if (_triggerFileFolder != null && _cacheRegistration.Count > 0)
            {
                throw new Exception("Unable to change folder after it has already been set!");
            }

            _triggerFileFolder = folderLocation;
        }

        public string GetTriggerFilePath<T>(string cacheKey)
        {
            var fileName = $"{cacheKey}.txt";

            SetTriggerFileDefaultIfNecessary();

            return Path.Combine(_triggerFileFolder, fileName);
        }

        public bool ShouldRefreshCache<T>(string cacheKey)
        {
            // Ensures we properly set up caches after application restarts
            if (!_cacheRegistration.ContainsKey(cacheKey))
            {
                return true;
            }
            else
            {
                var triggerFilePath = GetTriggerFilePath<T>(cacheKey);

                if (!File.Exists(triggerFilePath))
                    return true;

                var comparisonDt = File.GetLastWriteTime(triggerFilePath);
                var registerDt = _cacheRegistration[cacheKey];

                return comparisonDt != registerDt;
            }
        }

        public void TriggerCache<T>(string cacheKey)
        {
            var cachePath = GetTriggerFilePath<T>(cacheKey);
            if (File.Exists(cachePath))
            {
                File.Delete(cachePath);
            }
            RemoveFromCreatedHashes<T>(cacheKey);
        }

        #endregion

        #region Private Helper Methods

        private void AddToCreatedHashes<T>(string cacheKey)
        {
            if (_cacheRegistration.ContainsKey(cacheKey))
                return;

            var cacheFile = GetTriggerFilePath<T>(cacheKey);
            var cacheFileCreateDate = File.GetLastWriteTime(cacheFile);

            _cacheRegistration.Add(cacheKey, cacheFileCreateDate);
        }

        private void RemoveFromCreatedHashes<T>(string cacheKey)
        {
            if (!_cacheRegistration.ContainsKey(cacheKey))
                return;

            _cacheRegistration.Remove(cacheKey);
        }

        //private string GenerateKeyForCreatedCaches<T>(string cacheKey)
        //{
        //    var initial = typeof(T).FullName;

        //    if (string.IsNullOrEmpty(cacheKey))
        //    {
        //        return typeof(T).FullName;
        //    }
        //    else
        //    {
        //        return $"{initial}_{cacheKey}";
        //    }
        //}

        private void SetTriggerFileDefaultIfNecessary()
        {
            if (_triggerFileFolder == null)
            {
                _triggerFileFolder = _defaultTriggerFileFolder;
            }
        }

        #endregion
    }
}
