using System;

namespace JqD.Common
{
    public class CurrentTimeProvider : ICurrentTimeProvider
    {
        public DateTime CurrentTime()
        {
            return DateTime.Now;
        }
    }
}