using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDDPPP.Chap19.MicroORM.Application.Model.BidHistory;

namespace DDDPPP.Chap19.MicroORM.Application.Application.Queries
{
    public class AuctionStatus
    {
        public Guid Id { get; set; }       
        public decimal CurrentPrice { get; set; }
        public DateTime AuctionEnds { get; set; }
        public Guid WinningBidderId { get; set; }
        public int NumberOfBids { get; set; }
        public TimeSpan TimeRemaining { get; set; }
    }
}
