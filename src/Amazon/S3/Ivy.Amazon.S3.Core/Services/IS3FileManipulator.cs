using System.Threading.Tasks;

namespace Ivy.Amazon.S3.Core.Services
{
    public interface IS3FileManipulator
    {
        Task DeleteFileAsync(string bucketName, string objectKey);

        Task CopyFileAsync(string bucketName, string originalObjectKey, string newObjectKey);

        Task MoveFileAsync(string bucketName, string originalObjectKey, string newObjectKey);
    }
}
