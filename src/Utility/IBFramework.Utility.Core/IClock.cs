using System;

namespace IBFramework.Utility.Core
{
    public interface IClock
    {
        DateTime Now { get; }

        DateTime UtcNow { get; }

        DateTimeOffset NowWithOffset { get; }

        DateTimeOffset UtcNowWithOffset { get; }
    }
}
