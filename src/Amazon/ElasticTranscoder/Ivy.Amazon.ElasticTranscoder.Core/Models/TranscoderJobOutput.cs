namespace Ivy.Amazon.ElasticTranscoder.Core.Models
{
    /*
     * This is basically just a simple output wrapper, I'm not dealing with mapping individual types
     * If people want to map something out in their aplication, they can do it manually.
     * 
     * I plan on simply using a configuration object that maps to something in the appsettings.json file.
     */
    public class TranscoderJobOutput
    {
        public string OutputPrefix;

        public string PresetId;
    }
}
