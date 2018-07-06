using Ivy.Migration.Core.Interfaces.Services;
using Ivy.Migration.Test.Base;
using Ivy.TestUtilities.Utilities;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Ivy.Migration.Test.Services
{
    public class FileAccessorTests :
        MigrationTestBase<IFileAccessor>,
        IDisposable
    {
        #region SetUp & TearDown

        public FileAccessorTests()
        {
            SanitizeContext();
        }

        public void Dispose()
        {
            SanitizeContext();
        }

        #endregion

        #region Tests

        [Fact]
        public void FileAccessor_Throws_Execption_If_Directory_Doesnt_Exist()
        {
            var dir = GetTestDirectory();

            Assert.False(Directory.Exists(dir));

            var e = Assert.Throws<Exception>(() => Sut.GetDirectoryFiles(dir));

            var err = $"Unable to find directory! Path: {dir}";

            Assert.Equal(err, e.Message);
        }

        [Fact]
        public void FileAccessor_Returns_Contents_If_Exist()
        {
            var dir = GetTestDirectory();

            Directory.CreateDirectory(dir);

            Assert.True(Directory.Exists(dir));

            var files = Enumerable.Range(0, 3)
                .Select(x => $"{dir}Test{x}.txt");

            foreach (var file in files)
            {
                using (File.Create(file))
                {
                    Console.WriteLine($"Creating File: {file}");
                }
            }

            var results = Sut.GetDirectoryFiles(dir);

            AssertExtensions.FullBasicListExclusion(files, results);
        }

        #endregion

        #region Helper Methods

        private string GetTestDirectory()
        {
            var dir = Directory.GetCurrentDirectory();

            return dir + "\\Test\\";
        }

        private void SanitizeContext()
        {
            var dir = GetTestDirectory();

            // Do we have cleanup?
            if (Directory.Exists(dir))
            {
                var files = Directory.GetFiles(dir);

                // We must clear the directory before we delete it
                if (files.Any())
                {
                    foreach (var file in files)
                    {
                        File.Delete(file);
                    }
                }

                // Delete the directory when it's cleared
                Directory.Delete(dir);
            }
        }

        #endregion
    }
}
