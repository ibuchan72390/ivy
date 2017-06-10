using Amazon.S3;
using Amazon.S3.Model;
using Ivy.Amazon.S3.Core.Services;
using Ivy.Amazon.S3.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace Ivy.Amazon.S3.Test.Services
{
    public class S3FileValidatorTests : AmazonS3TestBase
    {
        #region Variables & Constants

        private readonly IS3FileValidator _sut;

        private readonly Mock<IAmazonS3> _mockS3Client;

        const string bucketName = "BucketName";
        const string objectKey = "ObjectKey";

        #endregion

        #region SetUp & TearDown

        public S3FileValidatorTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockS3Client = new Mock<IAmazonS3>();
            containerGen.RegisterInstance<IAmazonS3>(_mockS3Client.Object);

            var container = containerGen.GenerateContainer();

            _sut = container.Resolve<IS3FileValidator>();
        }

        #endregion

        #region Public Methods

        [Fact]
        public async void FileExistsAsync_Returns_True_If_Found()
        {
            _mockS3Client.Setup(x =>
                x.GetObjectMetadataAsync(It.Is<GetObjectMetadataRequest>(y => y.BucketName == bucketName && y.Key == objectKey), 
                    default(CancellationToken))
            ).ReturnsAsync(new GetObjectMetadataResponse());

            var result = await _sut.FileExistsAsync(bucketName, objectKey);

            Assert.True(result);
        }

        [Fact]
        public async void FileExistsAsync_Returns_False_If_Error_And_Not_Found_Status()
        {
            var exception = new AmazonS3Exception("TEST");
            exception.StatusCode = System.Net.HttpStatusCode.NotFound;

            _mockS3Client.Setup(x =>
                x.GetObjectMetadataAsync(It.Is<GetObjectMetadataRequest>(y => y.BucketName == bucketName && y.Key == objectKey),
                    default(CancellationToken))
            ).ThrowsAsync(exception);

            var result = await _sut.FileExistsAsync(bucketName, objectKey);

            Assert.False(result);
        }

        [Fact]
        public void FileExistsAsync_Throws_AmazonS3Exception_If_Error_And_Other_Than_Not_Found_Status()
        {
            var exception = new AmazonS3Exception("TEST");
            exception.StatusCode = System.Net.HttpStatusCode.Unauthorized;

            _mockS3Client.Setup(x =>
                x.GetObjectMetadataAsync(It.Is<GetObjectMetadataRequest>(y => y.BucketName == bucketName && y.Key == objectKey),
                    default(CancellationToken))
            ).ThrowsAsync(exception);

            var e = Assert.ThrowsAsync<AmazonS3Exception>(() => _sut.FileExistsAsync(bucketName, objectKey)).Result;

            Assert.Equal(exception.Message, e.Message);
        }


        [Fact]
        public void FileExistsAsync_Throws_Exception_If_Exception()
        {
            var exception = new Exception("TEST");

            _mockS3Client.Setup(x =>
                x.GetObjectMetadataAsync(It.Is<GetObjectMetadataRequest>(y => y.BucketName == bucketName && y.Key == objectKey),
                    default(CancellationToken))
            ).ThrowsAsync(exception);

            var e = Assert.ThrowsAsync<Exception>(() => _sut.FileExistsAsync(bucketName, objectKey)).Result;

            Assert.Equal(exception.Message, e.Message);
        }

        #endregion
    }
}
