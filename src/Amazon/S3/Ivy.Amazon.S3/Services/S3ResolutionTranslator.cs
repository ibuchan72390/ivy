using Ivy.Amazon.S3.Core.Enums;
using Ivy.Amazon.S3.Core.Services;
using System;

namespace Ivy.Amazon.S3.Services
{
    public class S3ResolutionTranslator : IS3ResolutionTranslator
    {
        public string GetResolutionString(ResolutionTypeName resolution)
        {
            switch (resolution)
            {
                case (ResolutionTypeName.Low):
                    return "480";
                case (ResolutionTypeName.Medium):
                    return "720";
                case (ResolutionTypeName.High):
                    return "1080";
                default:
                    throw new Exception($"Unmapped resolution received! - {resolution.ToString()}");
            }
        }
    }
}
