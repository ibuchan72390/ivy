using Xunit;
using System.IO;
using System;
using Ivy.TestHelper;
using Ivy.Caching.Test.Base;
using Ivy.Caching.Core.TriggerFile;

namespace Ivy.Caching.Test
{
    public class TriggerFileManagerTests : 
        TriggerFileCachingTestBase<ITriggerFileManager>, 
        IDisposable
    {
        #region Variables & Constants

        private const string cacheKey = "TESTCacheKey";

        #endregion

        #region Constructor

        public void Dispose()
        {
            if (Sut.TriggerFileFolder == null || !Directory.Exists(Sut.TriggerFileFolder))
                return;

            foreach (var file in Directory.GetFiles(Sut.TriggerFileFolder))
                File.Delete(file);
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
            var cacheFilePath = Sut.GetTriggerFilePath<T>(cacheKey);
            DeleteFile(cacheFilePath);
        }

        #endregion

        #region Tests

        #region GenerateTriggerFile

        [Fact]
        public void Trigger_File_Generator_Sets_Trigger_File_Name_If_Not_Set_Before_Generation()
        {
            var resultFile = Sut.GenerateTriggerFile<TestClass>(cacheKey);

            Assert.True(File.Exists(resultFile));

            DeleteFile(resultFile);
        }

        [Fact]
        public void Attempting_To_Generate_Trigger_File_Again_Does_Nothing()
        {
            var resultFile1 = Sut.GenerateTriggerFile<ITestInterface>(cacheKey);

            var resultFile2 = Sut.GenerateTriggerFile<ITestInterface>(cacheKey);

            Assert.True(File.Exists(resultFile2));

            Assert.Equal(resultFile1, resultFile2);

            DeleteFile(resultFile2);
        }

        [Fact]
        public void Setting_Trigger_File_Folder_Before_Generation_Generates_File_To_That_Location()
        {
            var targetFolder = "C:/Temp";

            Sut.SetTriggerFileFolder(targetFolder);

            var resultFile = Sut.GenerateTriggerFile<TestClass>(cacheKey);

            Assert.Contains(targetFolder, resultFile);

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

            Sut.SetTriggerFileFolder(folder1);

            Assert.Equal(folder1, Sut.TriggerFileFolder);

            Sut.SetTriggerFileFolder(folder2);

            Assert.Equal(folder2, Sut.TriggerFileFolder);
        }

        [Fact]
        public void Trigger_File_Generator_Can_Not_Change_Folder_After_File_Generation()
        {
            var currentDir = Directory.GetCurrentDirectory();

            var folder1 = $"{currentDir}/TestFolder3";
            var folder2 = $"{currentDir}/TestFolder4";

            Sut.SetTriggerFileFolder(folder1);

            Assert.Equal(folder1, Sut.TriggerFileFolder);

            Sut.GenerateTriggerFile<TestClass>(cacheKey);

            var e = Assert.Throws<Exception>(() => Sut.SetTriggerFileFolder(folder2));

            Assert.Equal("Unable to change folder after it has already been set!",
                e.Message);

            DeleteCacheFile<TestClass>();
        }

        #endregion

        #region ShouldRefreshCache

        [Fact]
        public void ShouldRefreshCache_True_If_Never_Registered()
        {
            Assert.True(Sut.ShouldRefreshCache<TestClass>(cacheKey));
        }

        [Fact]
        public void ShouldRefreshCache_False_If_File_Exists_Unchanged()
        {
            var triggerFile = Sut.GetTriggerFilePath<TestClass>(cacheKey);

            Assert.False(File.Exists(triggerFile));

            Sut.GenerateTriggerFile<TestClass>(cacheKey);

            Assert.False(Sut.ShouldRefreshCache<TestClass>(cacheKey));

            DeleteCacheFile<TestClass>();
        }

        [Fact]
        public void ShouldRefreshCache_True_If_Registered_And_File_Doesnt_Exist()
        {
            var triggerFile = Sut.GetTriggerFilePath<TestClass>(cacheKey);

            Assert.False(File.Exists(triggerFile));

            Sut.GenerateTriggerFile<TestClass>(cacheKey);

            Assert.False(Sut.ShouldRefreshCache<TestClass>(cacheKey));

            Assert.True(File.Exists(triggerFile));

            DeleteCacheFile<TestClass>();

            Assert.False(File.Exists(triggerFile));

            Assert.True(Sut.ShouldRefreshCache<TestClass>(cacheKey));
        }

        [Fact]
        public void ShouldRefreshCache_True_If_Registered_And_File_Is_Updated()
        {
            var triggerFile = Sut.GetTriggerFilePath<TestClass>(cacheKey);

            Assert.False(File.Exists(triggerFile));

            Sut.GenerateTriggerFile<TestClass>(cacheKey);

            Assert.False(Sut.ShouldRefreshCache<TestClass>(cacheKey));

            Assert.True(File.Exists(triggerFile));

            var origDate = File.GetLastWriteTime(triggerFile);
            File.SetLastWriteTime(triggerFile, DateTime.Now.AddDays(1));
            var newDate = File.GetLastWriteTime(triggerFile);

            Assert.True(newDate > origDate);

            Assert.True(Sut.ShouldRefreshCache<TestClass>(cacheKey));

            DeleteCacheFile<TestClass>();
        }

        #endregion

        #region TriggerCache

        [Fact]
        public void TriggerCache_Sets_ShouldRefresh_To_True()
        {
            Sut.GenerateTriggerFile<TestClass>(cacheKey);

            Assert.False(Sut.ShouldRefreshCache<TestClass>(cacheKey));

            Sut.TriggerCache<TestClass>(cacheKey);

            Assert.True(Sut.ShouldRefreshCache<TestClass>(cacheKey));

            DeleteCacheFile<TestClass>();
        }

        #endregion

        #endregion
    }
}
