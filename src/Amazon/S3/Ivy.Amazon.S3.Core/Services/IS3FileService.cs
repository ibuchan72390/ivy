using Ivy.Amazon.S3.Core.Enums;

namespace Ivy.Amazon.S3.Core.Services
{
    public interface IS3FileService
    {
        string GetCloudFrontSignedFileDownloadUrl(string bucketName, string objectKey);

        string GetCloudFrontSignedFileUploadUrl(string bucketName, string objectKey);

        string GetCloudFrontSignedVideoDownloadUrl(string bucketName, string objectKey, ResolutionTypeName resolution);

        string GetCloudFrontSignedVideoUploadUrl(string bucketName, string objectKey);
    }
}
