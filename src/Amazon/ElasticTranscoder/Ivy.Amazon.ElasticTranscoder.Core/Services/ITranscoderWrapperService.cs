using Ivy.Amazon.ElasticTranscoder.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ivy.Amazon.ElasticTranscoder.Core.Services
{
    /*
     * Only allows you to transcode within the context of a single bucket
     * That's fine as far as I'm concerned for now.
     * I think we can technically do one bucket per customer without any major performance issues
     */
    public interface ITranscoderWrapperService
    {
        Task<IEnumerable<TranscoderJobCreateResponse>> BeginTranscodeAsync(string startBucket, string startObject, IEnumerable<TranscoderJobOutput> outputs);
    }
}
