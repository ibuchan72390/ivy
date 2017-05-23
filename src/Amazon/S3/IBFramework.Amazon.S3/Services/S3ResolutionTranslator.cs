using iAMGlobalAngular2.Api.Core.Enums;
using IBFramework.Amazon.Core.S3.Services;
using System;

namespace iAMGlobalAngular2.Api.Data.Services.S3
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
