using Xunit;
using IBFramework.TestHelper;
using IBFramework.Core.Caching;
using System.IO;
using System;
using IBFramework.Core.IoC;
using IBFramework.IoC.Installers;

namespace IBFramework.Caching.Test
{
    public class TriggerFileManagerTests : TestBase, IDisposable
    {
        #region Variables & Constants

        private ITriggerFileManager _sut;

        #endregion

        #region Constructor

        public TriggerFileManagerTests()
        {
            _sut = TestServiceLocator.StaticContainer.Resolve<ITriggerFileManager>();
        }

        public void Dispose()
        {
            if (_sut.TriggerFileFolder == null || !Directory.Exists(_sut.TriggerFileFolder))
                return;


            foreach (var file in Directory.GetFiles(_sut.TriggerFileFolder))
                File.Delete(file);

            try
            {
                Directory.Delete(_sut.TriggerFileFolder);
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to delete {_sut.TriggerFileFolder}", e);
            }
        }

        private void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void DeleteCacheFile<T>()
        {
            var cacheFilePath = _sut.GetTriggerFilePath<T>();
            DeleteFile(cacheFilePath);
        }

        #endregion

        #region Tests

        #region GenerateTriggerFile

        [Fact]
        public void Trigger_File_Generator_Sets_Trigger_File_Name_If_Not_Set_Before_Generation()
        {
            var resultFile = _sut.GenerateTriggerFile<TestClass>();

            Assert.True(File.Exists(resultFile));

            DeleteFile(resultFile);
        }

        [Fact]
        public void Attempting_To_Generate_Trigger_File_Again_Does_Nothing()
        {
            var resultFile1 = _sut.GenerateTriggerFile<ITestInterface>();

            var resultFile2 = _sut.GenerateTriggerFile<ITestInterface>();

            Assert.True(File.Exists(resultFile2));

            Assert.Equal(resultFile1, resultFile2);

            DeleteFile(resultFile2);
        }

        [Fact]
        public void Setting_Trigger_File_Folder_Before_Generation_Generates_File_To_That_Location()
        {
            var targetFolder = "C:/Temp";

            _sut.SetTriggerFileFolder(targetFolder);

            var resultFile = _sut.GenerateTriggerFile<TestClass>();

            Assert.True(resultFile.Contains(targetFolder));

            Assert.True(File.Exists(resultFile));

            DeleteFile(resultFile);
        }

        #endregion

        #region SetTriggerFileFolder

        [Fact]
        public void Trigger_File_Generator_Can_Change_TriggerFileFolder_If_No_File_Generated_Yet()
        {
            var currentDir = Directory.GetCurrentDirectory();

            var folder1 = $"{currentDir}/TestFolder1";
            var folder2 = $"{currentDir}/TestFolder2";

            _sut.SetTriggerFileFolder(folder1);

            Assert.Equal(folder1, _sut.TriggerFileFolder);

            _sut.SetTriggerFileFolder(folder2);

            Assert.Equal(folder2, _sut.TriggerFileFolder);
        }

        [Fact]
        public void Trigger_File_Generator_Can_Not_Change_Folder_After_File_Generation()
        {
            var currentDir = Directory.GetCurrentDirectory();

            var folder1 = $"{currentDir}/TestFolder3";
            var folder2 = $"{currentDir}/TestFolder4";

            _sut.SetTriggerFileFolder(folder1);

            Assert.Equal(folder1, _sut.TriggerFileFolder);

            _sut.GenerateTriggerFile<TestClass>();

            var e = Assert.Throws<Exception>(() => _sut.SetTriggerFileFolder(folder2));

            Assert.Equal("Unable to change folder after it has already been set!",
                e.Message);

            DeleteCacheFile<TestClass>();
        }

        #endregion

        #region ShouldRefreshCache

        [Fact]
        public void ShouldRefreshCache_True_If_Never_Registered()
        {
            Assert.True(_sut.ShouldRefreshCache<TestClass>());
        }

        [Fact]
        public void ShouldRefreshCache_False_If_File_Exists_Unchanged()
        {
            var triggerFile = _sut.GetTriggerFilePath<TestClass>();

            Assert.False(File.Exists(triggerFile));

            _sut.GenerateTriggerFile<TestClass>();

            Assert.False(_sut.ShouldRefreshCache<TestClass>());

            DeleteCacheFile<TestClass>();
        }

        [Fact]
        public void ShouldRefreshCache_True_If_Registered_And_File_Doesnt_Exist()
        {
            var triggerFile = _sut.GetTriggerFilePath<TestClass>();

            Assert.False(File.Exists(triggerFile));

            _sut.GenerateTriggerFile<TestClass>();

            Assert.False(_sut.ShouldRefreshCache<TestClass>());

            Assert.True(File.Exists(triggerFile));

            DeleteCacheFile<TestClass>();

            Assert.False(File.Exists(triggerFile));

            Assert.True(_sut.ShouldRefreshCache<TestClass>());
        }

        [Fact]
        public void ShouldRefreshCache_True_If_Registered_And_File_Is_Updated()
        {
            var triggerFile = _sut.GetTriggerFilePath<TestClass>();

            Assert.False(File.Exists(triggerFile));

            _sut.GenerateTriggerFile<TestClass>();

            Assert.False(_sut.ShouldRefreshCache<TestClass>());

            Assert.True(File.Exists(triggerFile));

            var origDate = File.GetLastWriteTime(triggerFile);
            File.SetLastWriteTime(triggerFile, DateTime.Now.AddDays(1));
            var newDate = File.GetLastWriteTime(triggerFile);

            Assert.True(newDate > origDate);

            Assert.True(_sut.ShouldRefreshCache<TestClass>());

            DeleteCacheFile<TestClass>();
        }

        #endregion

        #region TriggerCache

        [Fact]
        public void TriggerCache_Sets_ShouldRefresh_To_True()
        {
            _sut.GenerateTriggerFile<TestClass>();

            Assert.False(_sut.ShouldRefreshCache<TestClass>());

            _sut.TriggerCache<TestClass>();

            Assert.True(_sut.ShouldRefreshCache<TestClass>());

            DeleteCacheFile<TestClass>();
        }

        #endregion

        #endregion
    }
}
