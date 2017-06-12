namespace Ivy.Amazon.ElasticTranscoder.Core.Providers
{
    /*
     * End user will have to build this object and populate it in their container
     * This will provide the required configurations for the local environment.
     */
    public interface ITranscoderConfigProvider
    {
        string PipelineId { get; }
    }
}
