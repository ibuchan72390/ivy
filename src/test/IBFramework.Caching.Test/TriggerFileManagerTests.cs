using Xunit;
using IBFramework.TestHelper;
using IBFramework.Core.Caching;
using System.IO;
using System;

namespace IBFramework.Caching.Test
{
    public class TriggerFileManagerTests : IDisposable
    {
        #region Variables & Constants

        private ITriggerFileManager _sut;

        private const string folder1 = "C:/Temp";

        #endregion

        #region Constructor

        public TriggerFileManagerTests()
        {
            // Using instance instead of resolution because
            // standard registration is a singleton instance
            _sut = new TriggerFileManager();
        }

        public void Dispose()
        {
            DeleteExpectedDirectory(folder1);
            DeleteExpectedDirectory(Directory.GetCurrentDirectory());
        }

        private void DeleteExpectedDirectory(string folder)
        {
            const string expectedDir = "AppCache";

            var expectedFolder = Path.Combine(folder, expectedDir);

            DeleteDirectoryIfExists(expectedFolder);
        }

        private void DeleteDirectoryIfExists(string path)
        {
            // This may be a bad idea at some point, but it was giving me
            // "used by another process" complaints.  This did it, and the
            // run time was about the same.
            if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    File.Delete(file);
                }

                Directory.Delete(path, true);
            }
        }

        private void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
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

            DeleteFile(resultFile1);
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
            var folder2 = "C:/Temp";

            _sut.SetTriggerFileFolder(folder1);

            Assert.Equal(folder1, _sut.TriggerFileFolder);

            _sut.SetTriggerFileFolder(folder2);

            Assert.Equal(folder2, _sut.TriggerFileFolder);
        }

        [Fact]
        public void Trigger_File_Generator_Can_Not_Change_Folder_After_File_Generation()
        {
            var folder2 = "C:/Temp";

            _sut.SetTriggerFileFolder(folder1);

            Assert.Equal(folder1, _sut.TriggerFileFolder);

            _sut.GenerateTriggerFile<TestClass>();

            var e = Assert.Throws<Exception>(() => _sut.SetTriggerFileFolder(folder2));

            Assert.Equal("Unable to change folder after it has already been set!",
                e.Message);
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
        }

        [Fact]
        public void ShouldRefreshCache_True_If_Registered_And_File_Doesnt_Exist()
        {
            var triggerFile = _sut.GetTriggerFilePath<TestClass>();

            Assert.False(File.Exists(triggerFile));

            _sut.GenerateTriggerFile<TestClass>();

            Assert.False(_sut.ShouldRefreshCache<TestClass>());

            Assert.True(File.Exists(triggerFile));

            File.Delete(_sut.GetTriggerFilePath<TestClass>());

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
        }

        #endregion

        #endregion
    }
}
