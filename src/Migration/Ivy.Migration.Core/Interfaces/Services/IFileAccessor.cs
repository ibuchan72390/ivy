using System.Collections.Generic;

/// <summary>
/// This is supposed to simply wrap the static File accessor class.
/// Testing the File system is extremely annoying and not something I feel like doing.
/// Eventually, this should probably become it's own library with injection functionality.
/// </summary>
namespace Ivy.Migration.Core.Interfaces.Services
{
    public interface IFileAccessor
    {
        IList<string> GetDirectoryFiles(string directoryPath);

        string GetFileText(string filePath);
    }
}
