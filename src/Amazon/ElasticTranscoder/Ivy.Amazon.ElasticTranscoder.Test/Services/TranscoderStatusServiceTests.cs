using Amazon.ElasticTranscoder;
using Amazon.ElasticTranscoder.Model;
using Ivy.Amazon.ElasticTranscoder.Core.Enums;
using Ivy.Amazon.ElasticTranscoder.Core.Services;
using Ivy.Amazon.ElasticTranscoder.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Ivy.TestUtilities;
using Ivy.Utility.Core.Extensions;
using Moq;
using System.Linq;
using System.Threading;
using Xunit;

namespace Ivy.Amazon.ElasticTranscoder.Test.Services
{
    public class TranscoderStatusServiceTests : ElasticTranscoderTestBase
    {
        #region Variables & Constants

        private readonly ITranscoderStatusService _sut;

        private readonly Mock<IAmazonElasticTranscoder> _mockTranscoder;

        #endregion

        #region SetUp & TearDown

        public TranscoderStatusServiceTests()
        {
            var containerGen = ServiceLocator.Instance.Resolve<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockTranscoder = new Mock<IAmazonElasticTranscoder>();
            containerGen.RegisterInstance<IAmazonElasticTranscoder>(_mockTranscoder.Object);

            _sut = containerGen.GenerateContainer().Resolve<ITranscoderStatusService>();
        }

        #endregion

        #region Tests

        #region ReadTranscoderJobAsync

        [Fact]
        public async void ReadTranscoderJobAsync_Maps_Properties_As_Expected()
        {
            const string jobId = "test";
            const TranscoderJobStatusName status = TranscoderJobStatusName.Submitted;

            var expectedResponse = new ReadJobResponse();
            expectedResponse.Job = new Job { Status = status.ToString() };

            _mockTranscoder.
                Setup(x => x.ReadJobAsync(It.Is<ReadJobRequest>(y => y.Id == jobId), default(CancellationToken))).
                ReturnsAsync(expectedResponse);

            var result = await _sut.ReadTranscoderJobAsync(jobId);

            _mockTranscoder.
                Verify(x => x.ReadJobAsync(It.Is<ReadJobRequest>(y => y.Id == jobId), default(CancellationToken)), Times.Once);

            Assert.Equal(jobId, result.JobId);
            Assert.Equal(status, result.Status);
        }

        #endregion

        #region ReadTranscoderJobsAsync

        [Fact]
        public async void ReadTranscoderJobsAsync_Maps_Properties_As_Expected()
        {
            const int jobCount = 3;
            const TranscoderJobStatusName status = TranscoderJobStatusName.Submitted;

            var jobIds = Enumerable.Range(1, jobCount).
                Select(x => x.ToString());

            var reqs = jobIds.
                Select(x => new ReadJobRequest { Id = x });

            var expectedResponse = new ReadJobResponse();
            expectedResponse.Job = new Job { Status = status.ToString() };

            _mockTranscoder.
                Setup(x => x.ReadJobAsync(It.IsAny<ReadJobRequest>(), default(CancellationToken))).
                ReturnsAsync(expectedResponse);

            var results = await _sut.ReadTranscoderJobsAsync(jobIds);
            var resultIds = results.Select(x => x.JobId);

            AssertExtensions.FullBasicListExclusion(jobIds, resultIds);

            results.Each(x => Assert.Equal(status, x.Status));

            _mockTranscoder.
                Verify(x => x.ReadJobAsync(It.IsAny<ReadJobRequest>(), default(CancellationToken)), 
                Times.Exactly(jobCount));
        }

        #endregion

        #endregion
    }
}
