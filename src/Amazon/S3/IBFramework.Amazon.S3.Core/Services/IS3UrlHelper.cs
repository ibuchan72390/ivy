namespace IBFramework.Amazon.Core.S3.Services
{
    public interface IS3UrlHelper
    {
        string GetS3Url(string region, string bucketName, string objectKey);
    }
}
