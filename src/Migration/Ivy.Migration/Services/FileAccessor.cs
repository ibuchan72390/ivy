using Ivy.Migration.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ivy.Migration.Services
{
    public class FileAccessor : IFileAccessor
    {
        public IList<string> GetDirectoryFiles(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new Exception($"Unable to find directory! Path: {directoryPath}");
            }

            return Directory.GetFiles(directoryPath).ToList();
        }

        public string GetFileText(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
