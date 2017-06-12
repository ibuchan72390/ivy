using Amazon.ElasticTranscoder;
using Amazon.ElasticTranscoder.Model;
using Ivy.Amazon.ElasticTranscoder.Core.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Ivy.Amazon.ElasticTranscoder.Core.Models;
using System.Collections.Generic;
using Ivy.Utility.Util;
using Ivy.Amazon.ElasticTranscoder.Core.Enums;

namespace Ivy.Amazon.ElasticTranscoder.Services
{
    public class TranscoderWrapperService : ITranscoderWrapperService
    {
        #region Variables & Constants

        private readonly IAmazonElasticTranscoder _transcoderSvc;

        #endregion

        #region Constructor

        public TranscoderWrapperService(
            IAmazonElasticTranscoder transcoderSvc)
        {
            _transcoderSvc = transcoderSvc;
        }

        #endregion

        #region Public Methods

        public async Task<IEnumerable<TranscoderJobCreateResponse>> BeginTranscodeAsync(string startBucket, string startObject, IEnumerable<TranscoderJobOutput> outputs)
        {
            var objectName = startObject.Split('/').Last();

            var req = new CreateJobRequest();

            req.Input = new JobInput();
            req.Input.Container = "auto";
            req.Input.Key = $"{startBucket}/{startObject}";

            req.Outputs = outputs.Select(
                x => new CreateJobOutput
                {
                    Key = $"{startBucket}/{x.OutputPrefix}/{objectName}",
                    PresetId = x.PresetId
                }).ToList();


            var response = await _transcoderSvc.CreateJobAsync(req);

            return response.Job.Outputs.Select(x => 
                new TranscoderJobCreateResponse
                {
                    JobId = x.Id,
                    PresetId = x.PresetId,
                    Status = EnumUtility.Parse<TranscoderJobStatusName>(x.Status)
                });
        }

        #endregion

    }
}
