using Amazon.S3;
using Amazon.S3.Model;
using Ivy.Amazon.S3.Core.Services;
using Ivy.Amazon.S3.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.Utility.Core;
using Moq;
using System;
using Xunit;

namespace Ivy.Amazon.S3.Test.Services
{
    public class S3SignedUrlServiceTests : AmazonS3TestBase
    {
        #region Variables & Constants

        private readonly IS3SignedUrlService _sut;

        private readonly Mock<IAmazonS3> _mockS3;
        private readonly Mock<IClock> _mockClock;

        private readonly DateTime now = new DateTime(2010, 10, 10);

        #endregion

        #region SetUp & TearDown

        public S3SignedUrlServiceTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockS3 = new Mock<IAmazonS3>();
            containerGen.RegisterInstance<IAmazonS3>(_mockS3.Object);

            _mockClock = new Mock<IClock>();
            containerGen.RegisterInstance<IClock>(_mockClock.Object);

            _mockClock.SetupGet(x => x.Now).Returns(now);

            var container = containerGen.GenerateContainer();

            _sut = container.Resolve<IS3SignedUrlService>();
        }

        #endregion

        #region Tests

        #region GetCloudFrontSignedFileDownloadUrl

        [Fact]
        public void GetCloudFrontSignedFileDownloadUrl_Passes_Params_As_Expected()
        {
            const string expectedReturn = "TEST";

            const string bucketName = "TESTBucket";
            const string key = "TESTKey";

            var expectedNow = now.AddMinutes(1);

            _mockS3.Setup(x => x.GetPreSignedURL(It.Is<GetPreSignedUrlRequest>(
                y => y.BucketName == bucketName &&
                     y.Key == key &&
                     y.Expires == expectedNow &&
                     y.Verb == HttpVerb.GET))).Returns(expectedReturn);

            var result = _sut.GetS3SignedFileDownloadUrl(bucketName, key);

            Assert.Equal(expectedReturn, result);

            _mockS3.Verify(x => x.GetPreSignedURL(It.Is<GetPreSignedUrlRequest>(
                y => y.BucketName == bucketName &&
                     y.Key == key &&
                     y.Expires == expectedNow &&
                     y.Verb == HttpVerb.GET)), Times.Once);
        }

        [Fact]
        public void GetCloudFrontSignedFileDownloadUrl_Passes_Params_As_Expected_With_File_Override()
        {
            const string expectedReturn = "TEST";

            const string bucketName = "TESTBucket";
            const string key = "TESTKey";
            const string nameOverride = "TESTOverride.txt";

            string expectedContentDisposition = $"attachment: filename={nameOverride}";

            var expectedNow = now.AddMinutes(1);

            _mockS3.Setup(x => x.GetPreSignedURL(It.Is<GetPreSignedUrlRequest>(
                y => y.BucketName == bucketName &&
                     y.Key == key &&
                     y.Expires == expectedNow &&
                     y.Verb == HttpVerb.GET &&
                     y.ResponseHeaderOverrides.ContentDisposition == expectedContentDisposition))).Returns(expectedReturn);

            var result = _sut.GetS3SignedFileDownloadUrl(bucketName, key, nameOverride);

            Assert.Equal(expectedReturn, result);

            _mockS3.Verify(x => x.GetPreSignedURL(It.Is<GetPreSignedUrlRequest>(
                y => y.BucketName == bucketName &&
                     y.Key == key &&
                     y.Expires == expectedNow &&
                     y.Verb == HttpVerb.GET &&
                     y.ResponseHeaderOverrides.ContentDisposition == expectedContentDisposition)), Times.Once);
        }

        #endregion

        #region GetCloudFrontSignedFileUploadUrl

        [Fact]
        public void GetCloudFrontSignedFileUploadUrl_Passes_Params_As_Expected()
        {
            const string expectedReturn = "TEST";

            const string bucketName = "TESTBucket";
            const string key = "TESTKey";

            var expectedNow = now.AddMinutes(1);

            _mockS3.Setup(x => x.GetPreSignedURL(It.Is<GetPreSignedUrlRequest>(
                y => y.BucketName == bucketName &&
                     y.Key == key &&
                     y.Expires == expectedNow &&
                     y.Verb == HttpVerb.PUT
                ))).Returns(expectedReturn);

            var result = _sut.GetS3SignedFileUploadUrl(bucketName, key);

            Assert.Equal(expectedReturn, result);

            _mockS3.Verify(x => x.GetPreSignedURL(It.Is<GetPreSignedUrlRequest>(
                y => y.BucketName == bucketName &&
                     y.Key == key &&
                     y.Expires == expectedNow &&
                     y.Verb == HttpVerb.PUT
                )), Times.Once);
        }

        #endregion

        #endregion
    }
}
