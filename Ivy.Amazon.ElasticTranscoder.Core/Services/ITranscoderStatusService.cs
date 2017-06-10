using Ivy.Amazon.ElasticTranscoder.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ivy.Amazon.ElasticTranscoder.Core.Services
{
    public interface ITranscoderStatusService
    {
        Task<TranscoderJobStatusResponse> ReadTranscoderJobAsync(string jobId);

        Task<IEnumerable<TranscoderJobStatusResponse>> ReadTranscoderJobsAsync(IEnumerable<string> jobIds);
    }
}
