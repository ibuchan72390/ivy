using Ivy.Amazon.S3.Core.Services;

namespace Ivy.Amazon.S3.Services
{
    public class S3UrlHelper : IS3UrlHelper
    {
        public string GetS3Url(string region, string bucketName, string objectKey)
        {
            return $"http://{bucketName}.s3-{region}.amazon.aws.com/{objectKey}";
        }
    }
}
