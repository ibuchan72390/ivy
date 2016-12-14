using IBFramework.Core.Caching;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBFramework.Caching
{
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

        public string GenerateTriggerFile<T>(string cacheKey = null)
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

        public string GetTriggerFilePath<T>(string cacheKey = null)
        {
            var fileName = GenerateKeyForCreatedCaches<T>(cacheKey) + ".txt";

            SetTriggerFileDefaultIfNecessary();

            return Path.Combine(_triggerFileFolder, fileName);
        }

        public bool ShouldRefreshCache<T>(string cacheKey = null)
        {
            var createdCacheKey = GenerateKeyForCreatedCaches<T>(cacheKey);

            // Ensures we properly set up caches after application restarts
            if (!_cacheRegistration.ContainsKey(createdCacheKey))
            {
                return true;
            }
            else
            {
                var triggerFilePath = GetTriggerFilePath<T>(cacheKey);

                if (!File.Exists(triggerFilePath))
                    return true;

                var comparisonDt = File.GetLastWriteTime(triggerFilePath);
                var registerDt = _cacheRegistration[createdCacheKey];

                return comparisonDt != registerDt;
            }
        }

        public void TriggerCache<T>(string cacheKey = null)
        {
            var cachePath = GetTriggerFilePath<T>();
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
            var generatedCachesKey = GenerateKeyForCreatedCaches<T>(cacheKey);

            if (_cacheRegistration.ContainsKey(generatedCachesKey))
                return;


            var cacheFile = GetTriggerFilePath<T>(cacheKey);
            var cacheFileCreateDate = File.GetLastWriteTime(cacheFile);

            _cacheRegistration.Add(generatedCachesKey, cacheFileCreateDate);
        }

        private void RemoveFromCreatedHashes<T>(string cacheKey)
        {
            var generatedCachesKey = GenerateKeyForCreatedCaches<T>(cacheKey);

            if (!_cacheRegistration.ContainsKey(generatedCachesKey))
                return;

            _cacheRegistration.Remove(generatedCachesKey);
        }

        private string GenerateKeyForCreatedCaches<T>(string cacheKey)
        {
            var initial = typeof(T).FullName;

            if (string.IsNullOrEmpty(cacheKey))
            {
                return typeof(T).FullName;
            }
            else
            {
                return $"{initial}_{cacheKey}";
            }
        }

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
