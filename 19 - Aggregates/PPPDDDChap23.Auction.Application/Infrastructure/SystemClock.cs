using System;

namespace PPPDDDChap23.Auction.Application.Infrastructure
{
   public class SystemClock : IClock
    {
        public DateTime Time()
        {
            return DateTime.Now;
        }
    }
}
