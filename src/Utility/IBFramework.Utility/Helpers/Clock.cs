using IBFramework.Utility.Core;
using System;

namespace IBFramework.Utility
{
    public class Clock : IClock
    {
        public DateTime Now => DateTime.Now;

        public DateTimeOffset NowWithOffset => DateTimeOffset.Now;

        public DateTime UtcNow => DateTime.UtcNow;

        public DateTimeOffset UtcNowWithOffset => DateTimeOffset.UtcNow;
    }
}
