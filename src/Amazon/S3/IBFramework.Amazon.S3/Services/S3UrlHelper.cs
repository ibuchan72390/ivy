using IBFramework.Amazon.Core.S3.Services;

namespace iAMGlobalAngular2.Api.Data.Services.S3
{
    public class S3UrlHelper : IS3UrlHelper
    {
        public string GetS3Url(string region, string bucketName, string objectKey)
        {
            return $"http://{bucketName}.s3-{region}.amazon.aws.com/{objectKey}";
        }
    }
}
