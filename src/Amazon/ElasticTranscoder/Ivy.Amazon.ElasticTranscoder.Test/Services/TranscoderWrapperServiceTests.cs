using Amazon.ElasticTranscoder;
using Amazon.ElasticTranscoder.Model;
using Ivy.Amazon.ElasticTranscoder.Core.Enums;
using Ivy.Amazon.ElasticTranscoder.Core.Models;
using Ivy.Amazon.ElasticTranscoder.Core.Providers;
using Ivy.Amazon.ElasticTranscoder.Core.Services;
using Ivy.Amazon.ElasticTranscoder.Test.Base;
using Ivy.IoC;
using Ivy.IoC.Core;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Ivy.Amazon.ElasticTranscoder.Test.Services
{
    public class TranscoderWrapperServiceTests : ElasticTranscoderTestBase
    {
        #region Variables & Constants

        private readonly ITranscoderWrapperService _sut;

        private readonly Mock<IAmazonElasticTranscoder> _mockTranscoder;
        private readonly Mock<ITranscoderConfigProvider> _mockConfig;

        const string pipelineId = "PipelineId";

        const string startBucket = "StartBucket";
        const string startKey = "StartKey";

        #endregion

        #region SetUp & TearDown

        public TranscoderWrapperServiceTests()
        {
            var containerGen = ServiceLocator.Instance.GetService<IContainerGenerator>();

            base.ConfigureContainer(containerGen);

            _mockTranscoder = new Mock<IAmazonElasticTranscoder>();
            containerGen.RegisterInstance<IAmazonElasticTranscoder>(_mockTranscoder.Object);

            _mockConfig = new Mock<ITranscoderConfigProvider>();
            containerGen.RegisterInstance<ITranscoderConfigProvider>(_mockConfig.Object);

            _sut = containerGen.GenerateContainer().GetService<ITranscoderWrapperService>();


            _mockConfig.Setup(x => x.PipelineId).Returns(pipelineId);
        }

        #endregion

        #region Tests

        #region BeginTranscodeAsync

        [Fact]
        public async void BeginTranscodeAsync_Returns_Empty_If_Empty_Outputs()
        {
            const int toCreate = 3;

            var ids = Enumerable.Range(0, toCreate);

            var outputs = ids.Select(
                x => new TranscoderJobOutput { OutputPrefix = $"Prefix{x}", PresetId = $"Preset{x}" }
            );

            var response = new CreateJobResponse();
            response.Job = new Job();
            response.Job.Outputs = new List<JobOutput>();

            _mockTranscoder.Setup(x => x.CreateJobAsync(It.Is<CreateJobRequest>(y =>
                y.Input.Container == "auto" &&
                y.Input.Key == startKey &&
                y.Outputs.Count == 0
            ), default(CancellationToken))).ReturnsAsync(response);

            var results = await _sut.BeginTranscodeAsync(startBucket, startKey, new List<TranscoderJobOutput>());

            Assert.Empty(results);

            _mockTranscoder.Verify(x => x.CreateJobAsync(It.Is<CreateJobRequest>(y =>
               y.Input.Container == "auto" &&
               y.Input.Key == startKey &&
               y.Outputs.Count == 0
           ), default(CancellationToken)), Times.Once);
        }

        [Fact]
        public async void BeginTranscodeAsync_Creates_Requests_As_Expected()
        {
            const int toCreate = 3;
            const TranscoderJobStatusName resultStatus = TranscoderJobStatusName.Error;

            var ids = Enumerable.Range(0, toCreate);

            var outputs = ids.Select(
                x => new TranscoderJobOutput { OutputPrefix = $"Prefix{x}", PresetId = $"Preset{x}" }
            );

            var response = new CreateJobResponse();
            response.Job = new Job { Id = "1497729881641-1npjp0" };
            response.Job.Outputs = outputs.Select(x => new JobOutput { PresetId = x.PresetId, Status = resultStatus.ToString() }).ToList();

            var outputPresets = outputs.Select(x => x.PresetId);

            _mockTranscoder.Setup(x => x.CreateJobAsync(It.Is<CreateJobRequest>(y => 
                y.Input.Container == "auto" &&
                y.Input.Key == startKey &&
                !y.Outputs.Select(z => z.PresetId).Except(outputPresets).Any()
            ), default(CancellationToken))).ReturnsAsync(response);

            var results = await _sut.BeginTranscodeAsync(startBucket, startKey, outputs);

            Assert.Equal(toCreate, results.Count());

            foreach (var output in results)
            {
                Assert.Equal(response.Job.Id, output.ParentJobId);
                Assert.True(outputPresets.Contains(output.PresetId));
                Assert.Equal(resultStatus, output.Status);
            }

            _mockTranscoder.Verify(x => x.CreateJobAsync(It.Is<CreateJobRequest>(y =>
                y.Input.Container == "auto" &&
                y.Input.Key == startKey &&
                !y.Outputs.Select(z => z.PresetId).Except(outputPresets).Any()
            ), default(CancellationToken)), Times.Once);
        }

        #endregion

        #endregion
    }
}
