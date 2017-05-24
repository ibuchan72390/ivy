using Ivy.Amazon.S3.Core.Enums;

namespace Ivy.Amazon.S3.Core.Services
{
    public interface IS3FileService
    {
        string GetCloudFrontSignedFileUrl(string bucketName, string objectKey);

        string GetCloudFrontSignedVideoUrl(string bucketName, string objectKey, ResolutionTypeName resolution);
    }
}
