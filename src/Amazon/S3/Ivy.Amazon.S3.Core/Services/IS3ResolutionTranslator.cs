using Ivy.Amazon.S3.Core.Enums;

namespace Ivy.Amazon.S3.Core.Services
{
    public interface IS3ResolutionTranslator
    {
        string GetResolutionString(ResolutionTypeName resolution);
    }
}
