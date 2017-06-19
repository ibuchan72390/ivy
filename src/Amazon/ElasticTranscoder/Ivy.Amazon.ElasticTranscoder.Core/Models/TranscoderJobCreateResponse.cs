using Ivy.Amazon.ElasticTranscoder.Core.Enums;

namespace Ivy.Amazon.ElasticTranscoder.Core.Models
{
    public class TranscoderJobCreateResponse
    {
        public string ParentJobId; 

        public string JobId;

        public string PresetId;

        public TranscoderJobStatusName Status;
    }
}
