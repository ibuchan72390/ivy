using Amazon.ElasticTranscoder;
using Amazon.ElasticTranscoder.Model;
using Ivy.Amazon.ElasticTranscoder.Core.Services;
using System.Linq;
using System.Threading.Tasks;
using Ivy.Amazon.ElasticTranscoder.Core.Models;
using System.Collections.Generic;
using Ivy.Amazon.ElasticTranscoder.Core.Enums;
using Ivy.Amazon.ElasticTranscoder.Core.Providers;
using Ivy.Utility.Core.Util;
using Ivy.Utility.Core.Extensions;

namespace Ivy.Amazon.ElasticTranscoder.Services
{
    public class TranscoderWrapperService : ITranscoderWrapperService
    {
        #region Variables & Constants

        private readonly IAmazonElasticTranscoder _transcoderSvc;
        private readonly ITranscoderConfigProvider _configProvider;

        #endregion

        #region Constructor

        public TranscoderWrapperService(
            IAmazonElasticTranscoder transcoderSvc,
            ITranscoderConfigProvider configProvider)
        {
            _transcoderSvc = transcoderSvc;
            _configProvider = configProvider;
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<TranscoderJobCreateResponse>> BeginTranscodeAsync(string startBucket, 
            string startObject, IEnumerable<TranscoderJobOutput> outputs)
        {
            var objectName = startObject.Split('/').Last();

            var req = new CreateJobRequest();

            req.PipelineId = _configProvider.PipelineId;

            req.Input = new JobInput();
            req.Input.Container = "auto";
            req.Input.Key = $"{startObject}";

            req.Outputs = outputs.Select(
                x => new CreateJobOutput
                {
                    Key = $"{x.OutputPrefix}/{objectName}",
                    PresetId = x.PresetId
                }).ToList();


            var response = await _transcoderSvc.CreateJobAsync(req);

            var responses = response.Job.Outputs.Select(x => 
                new TranscoderJobCreateResponse
                {
                    JobId = x.Id,
                    PresetId = x.PresetId,
                    Status = EnumUtility.Parse<TranscoderJobStatusName>(x.Status)
                }).ToList();

            responses.Each(x => x.ParentJobId = response.Job.Id);

            return responses;
        }

        #endregion

    }
}
