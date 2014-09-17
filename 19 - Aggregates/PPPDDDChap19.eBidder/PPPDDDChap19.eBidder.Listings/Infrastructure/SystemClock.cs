using System;

namespace PPPDDDChap19.eBidder.Listings.Application.Infrastructure
{
   public class SystemClock : IClock
    {
        public DateTime Time()
        {
            return DateTime.Now;
        }
    }
}
