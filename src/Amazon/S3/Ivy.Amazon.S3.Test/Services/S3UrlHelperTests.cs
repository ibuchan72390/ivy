using Ivy.Amazon.S3.Core.Services;
using Ivy.Amazon.S3.Test.Base;
using Ivy.IoC;
using Xunit;

namespace Ivy.Amazon.S3.Test.Services
{
    public class S3UrlHelperTests : 
        AmazonS3TestBase<IS3UrlHelper>
    {
        #region Tests

        [Fact]
        public void GetS3Url_Works_As_Expected()
        {
            const string region = "TESTRegion";
            const string bucket = "TESTBucket";
            const string key = "TESTKey";

            var result = Sut.GetS3Url(region, bucket, key);

            var expected = $"http://{bucket}.s3-{region}.amazon.aws.com/{key}";

            Assert.Equal(expected, result);
        }


        #endregion
    }
}
