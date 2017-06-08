using Ivy.Amazon.S3.Core.Enums;

namespace Ivy.Amazon.S3.Core.Services
{
    public interface IS3VideoKeyGenerator
    {
        string GetS3VideoDownloadKey(string objectKey, ResolutionTypeName resolution);

        string GetS3VideoUploadKey(string objectKey);
    }
}
