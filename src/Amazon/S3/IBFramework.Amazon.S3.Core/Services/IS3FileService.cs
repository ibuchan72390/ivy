namespace IBFramework.Amazon.Core.S3.Services
{
    public interface IS3FileService
    {
        string GetCloudFrontSignedFileUrl(string bucketName, string objectKey);

        string GetCloudFrontSignedVideoUrl(string bucketName, string objectKey, string resolution);
    }
}
