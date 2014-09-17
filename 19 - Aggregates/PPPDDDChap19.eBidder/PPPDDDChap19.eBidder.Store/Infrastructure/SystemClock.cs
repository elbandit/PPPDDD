using System;

namespace PPPDDDChap19.eBidder.Store.Application.Infrastructure
{
   public class SystemClock : IClock
    {
        public DateTime Time()
        {
            return DateTime.Now;
        }
    }
}
