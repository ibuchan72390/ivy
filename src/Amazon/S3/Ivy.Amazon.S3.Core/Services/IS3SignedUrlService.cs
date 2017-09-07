namespace Ivy.Amazon.S3.Core.Services
{
    public interface IS3SignedUrlService
    {
        string GetS3SignedFileDownloadUrl(string bucketName, string objectKey, string downloadFileNameOverride = null);

        string GetS3SignedFileUploadUrl(string bucketName, string objectKey);
    }
}
