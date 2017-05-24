using Ivy.Utility.Core;
using System;

namespace Ivy.Utility
{
    public class Clock : IClock
    {
        public DateTime Now => DateTime.Now;

        public DateTimeOffset NowWithOffset => DateTimeOffset.Now;

        public DateTime UtcNow => DateTime.UtcNow;

        public DateTimeOffset UtcNowWithOffset => DateTimeOffset.UtcNow;
    }
}
