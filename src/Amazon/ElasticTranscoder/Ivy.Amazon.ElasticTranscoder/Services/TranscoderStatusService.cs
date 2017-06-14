using Amazon.ElasticTranscoder;
using Amazon.ElasticTranscoder.Model;
using Ivy.Amazon.ElasticTranscoder.Core.Enums;
using Ivy.Amazon.ElasticTranscoder.Core.Models;
using Ivy.Amazon.ElasticTranscoder.Core.Services;
using Ivy.Utility.Core.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ivy.Amazon.ElasticTranscoder.Services
{
    public class TranscoderStatusService : ITranscoderStatusService
    {
        #region Variables & Constants

        private readonly IAmazonElasticTranscoder _transcoder;

        #endregion

        #region Constructor

        public TranscoderStatusService(
            IAmazonElasticTranscoder transcoder)
        {
            _transcoder = transcoder;
        }


        #endregion

        #region Public Methods

        public async Task<TranscoderJobStatusResponse> ReadTranscoderJobAsync(string jobId)
        {
            var jobReadRequest = new ReadJobRequest();

            jobReadRequest.Id = jobId;

            var response = await _transcoder.ReadJobAsync(jobReadRequest);

            return new TranscoderJobStatusResponse { JobId = jobId, Status =  EnumUtility.Parse<TranscoderJobStatusName>(response.Job.Status) };
        }

        public async Task<IEnumerable<TranscoderJobStatusResponse>> ReadTranscoderJobsAsync(IEnumerable<string> jobIds)
        {
            IList<TranscoderJobStatusResponse> responses = new List<TranscoderJobStatusResponse>();

            foreach (var id in jobIds)
            {
                responses.Add(await ReadTranscoderJobAsync(id));
            }

            return responses;
        }

        #endregion
    }
}
