namespace Ivy.Amazon.S3.Core.Services
{
    public interface IS3UrlHelper
    {
        string GetS3Url(string region, string bucketName, string objectKey);
    }
}
