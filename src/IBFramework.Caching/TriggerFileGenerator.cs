using IBFramework.Core.Caching;
using System;
using System.IO;

namespace IBFramework.Caching
{
    public class TriggerFileGenerator : ITriggerFileGenerator
    {
        #region Variables & Constants

        const string defaultRootPath = "AppCache";

        private string _triggerFileFolder = null;

        private readonly string _defaultTriggerFileFolder;
        private bool _fileCreated = false;

        public string TriggerFileFolder => _triggerFileFolder;

        #endregion

        #region Constructor

        public TriggerFileGenerator()
        {
            var appRoot = Directory.GetCurrentDirectory();
            _defaultTriggerFileFolder = Path.Combine(appRoot, defaultRootPath);
        }

        #endregion

        #region Public Methods

        public string GenerateTriggerFile<T>(string nameKey = null, string fileLocation = null)
        {
            if (_triggerFileFolder == null)
            {
                _triggerFileFolder = _defaultTriggerFileFolder;
            }

            var fileName = typeof(T) + ".txt";
            var fileToCreate = Path.Combine(_triggerFileFolder, fileName);

            var fileResult = File.CreateText(fileToCreate);

            _fileCreated = true;

            return fileToCreate;
        }

        public void SetTriggerFileFolder(string folderLocation)
        {
            if (_triggerFileFolder != null && _fileCreated)
            {
                throw new Exception("Unable to change folder after it has already been set!");
            }

            _triggerFileFolder = folderLocation;
        }

        #endregion
    }
}
