using Amazon.S3;
using Amazon.S3.Model;
using Ivy.Amazon.S3.Core.Enums;
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
    public class S3FileServiceTests : AmazonS3TestBase
    {
        #region Variables & Constants

        private readonly IS3FileService _sut;

        private readonly Mock<IAmazonS3> _mockS3;
        private readonly Mock<IClock> _mockClock;

        private readonly DateTime now = new DateTime(2010, 10, 10);

        #endregion

        #region SetUp & TearDown

        public S3FileServiceTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockS3 = new Mock<IAmazonS3>();
            containerGen.RegisterInstance<IAmazonS3>(_mockS3.Object);

            _mockClock = new Mock<IClock>();
            containerGen.RegisterInstance<IClock>(_mockClock.Object);

            _mockClock.SetupGet(x => x.Now).Returns(now);

            var container = containerGen.GenerateContainer();

            _sut = container.Resolve<IS3FileService>();
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

            var result = _sut.GetCloudFrontSignedFileDownloadUrl(bucketName, key);

            Assert.Equal(expectedReturn, result);

            _mockS3.Verify(x => x.GetPreSignedURL(It.Is<GetPreSignedUrlRequest>(
                y => y.BucketName == bucketName &&
                     y.Key == key &&
                     y.Expires == expectedNow &&
                     y.Verb == HttpVerb.GET)), Times.Once);
        }

        #endregion

        #region GetCloudFrontSignedVideoDownloadUrl

        [Fact]
        public void GetCloudFrontSignedVideoDownloadUrl_Passes_Params_As_Expected()
        {
            const ResolutionTypeName resolution = ResolutionTypeName.High;
            const string expectedReturn = "TEST";
            const string bucketName = "TESTBucket";
            const string key = "TESTKey";

            var videoKeyGen = ServiceLocator.Instance.Resolve<IS3VideoKeyGenerator>();

            string expectedKey = videoKeyGen.GetS3VideoDownloadKey(key, resolution);

            var expectedNow = now.AddMinutes(1);

            _mockS3.Setup(x => x.GetPreSignedURL(It.Is<GetPreSignedUrlRequest>(
                y => y.BucketName == bucketName &&
                     y.Key == expectedKey &&
                     y.Expires == expectedNow &&
                     y.Verb == HttpVerb.GET
                ))).Returns(expectedReturn);

            var result = _sut.GetCloudFrontSignedVideoDownloadUrl(bucketName, key, resolution);

            Assert.Equal(expectedReturn, result);

            _mockS3.Verify(x => x.GetPreSignedURL(It.Is<GetPreSignedUrlRequest>(
                y => y.BucketName == bucketName &&
                     y.Key == expectedKey &&
                     y.Expires == expectedNow &&
                     y.Verb == HttpVerb.GET
                )), Times.Once);
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

            var result = _sut.GetCloudFrontSignedFileUploadUrl(bucketName, key);

            Assert.Equal(expectedReturn, result);

            _mockS3.Verify(x => x.GetPreSignedURL(It.Is<GetPreSignedUrlRequest>(
                y => y.BucketName == bucketName &&
                     y.Key == key &&
                     y.Expires == expectedNow &&
                     y.Verb == HttpVerb.PUT
                )), Times.Once);
        }

        #endregion

        #region GetCloudFrontSignedVideoUploadUrl

        [Fact]
        public void GetCloudFrontSignedVideoUploadUrl_Passes_Params_As_Expected()
        {
            const string expectedReturn = "TEST";
            const string bucketName = "TESTBucket";
            const string key = "TESTKey";

            var videoKeyGen = ServiceLocator.Instance.Resolve<IS3VideoKeyGenerator>();

            string expectedKey = videoKeyGen.GetS3VideoUploadKey(key);

            var expectedNow = now.AddMinutes(1);

            _mockS3.Setup(x => x.GetPreSignedURL(It.Is<GetPreSignedUrlRequest>(
                y => y.BucketName == bucketName &&
                     y.Key == expectedKey &&
                     y.Expires == expectedNow &&
                     y.Verb == HttpVerb.PUT
                ))).Returns(expectedReturn);

            var result = _sut.GetCloudFrontSignedVideoUploadUrl(bucketName, key);

            Assert.Equal(expectedReturn, result);

            _mockS3.Verify(x => x.GetPreSignedURL(It.Is<GetPreSignedUrlRequest>(
                y => y.BucketName == bucketName &&
                     y.Key == expectedKey &&
                     y.Expires == expectedNow &&
                     y.Verb == HttpVerb.PUT
                )), Times.Once);
        }

        #endregion

        #endregion
    }
}
