using System.IO;
using System.Threading.Tasks;

namespace Ivy.Amazon.S3.Core.Services
{
    public interface IS3FileAccessor
    {
        Task<Stream> GetFileStreamFromS3Async(string bucketName, string objectKey);

        Task SaveFileStreamToS3Async(string bucketName, string objectKey, Stream fileStream);
    }
}
