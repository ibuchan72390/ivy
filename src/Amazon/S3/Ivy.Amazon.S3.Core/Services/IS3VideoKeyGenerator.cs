using Ivy.Amazon.S3.Core.Enums;

namespace Ivy.Amazon.S3.Core.Services
{
    public interface IS3VideoKeyGenerator
    {
        string GetS3VideoKey(string objectKey, ResolutionTypeName resolution);
    }
}
