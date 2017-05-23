using iAMGlobalAngular2.Api.Core.Enums;

namespace IBFramework.Amazon.Core.S3.Services
{
    public interface IS3ResolutionTranslator
    {
        string GetResolutionString(ResolutionTypeName resolution);
    }
}
