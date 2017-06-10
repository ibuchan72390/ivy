using System.Threading.Tasks;

namespace Ivy.Amazon.S3.Core.Services
{
    public interface IS3FileValidator
    {
        Task<bool> FileExistsAsync(string bucketName, string objectKey);
    }
}
