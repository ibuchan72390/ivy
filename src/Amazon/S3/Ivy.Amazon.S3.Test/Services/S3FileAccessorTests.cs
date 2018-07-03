using Amazon.S3;
using Ivy.Amazon.S3.Core.Services;
using Ivy.Amazon.S3.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ivy.Amazon.S3.Test.Services
{
    public class S3FileAccessorTests : 
        AmazonS3TestBase<IS3FileAccessor>
    {
        #region Variables & Constants

        private Mock<IAmazonS3> _mockS3;

        const string bucket = "bucket";
        const string objKey = "objkey";

        #endregion

        #region SetUp & TearDown

        protected override void InitializeContainerFn(IContainerGenerator containerGen)
        {
            base.InitializeContainerFn(containerGen);

            _mockS3 = InitializeMoq<IAmazonS3>(containerGen);
        }

        #endregion

        #region Tests

        #region GetFileStreamFromS3Async

        [Fact]
        public async void GetFileStreamFromS3Async_Executes_As_Expected()
        {
            using (var testStream = new MemoryStream())
            {
                _mockS3
                    .Setup(x => x.GetObjectStreamAsync(bucket, objKey, It.Is<IDictionary<string, object>>(y => y.Count == 0), default(CancellationToken)))
                    .ReturnsAsync(testStream);

                var result = await Sut.GetFileStreamFromS3Async(bucket, objKey);

                Assert.Same(testStream, result);

                _mockS3.Verify(x => x.GetObjectStreamAsync(bucket, objKey, It.Is<IDictionary<string, object>>(y => y.Count == 0), default(CancellationToken)), Times.Once);
            }
        }

        #endregion

        #region SaveStreamToS3Async

        [Fact]
        public async void SaveStreamToS3Async_Executes_As_Expected()
        {
            using (var testStream = new MemoryStream())
            {
                _mockS3
                    .Setup(x => x.UploadObjectFromStreamAsync(bucket, objKey, testStream, It.Is<IDictionary<string, object>>(y => y.Count == 0), default(CancellationToken)))
                    .Returns(Task.FromResult(0));

                await Sut.SaveFileStreamToS3Async(bucket, objKey, testStream);

                _mockS3
                    .Verify(x => x.UploadObjectFromStreamAsync(bucket, objKey, testStream, It.Is<IDictionary<string, object>>(y => y.Count == 0), default(CancellationToken)),
                        Times.Once);

            }
        }

        #endregion

        #endregion
    }
}
