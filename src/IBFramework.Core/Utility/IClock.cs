using System;

namespace IBFramework.Core.Utility
{
    public interface IClock
    {
        DateTime Now { get; }

        DateTime UtcNow { get; }

        DateTimeOffset NowWithOffset { get; }

        DateTimeOffset UtcNowWithOffset { get; }
    }
}
