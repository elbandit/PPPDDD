using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Store.Application.Application.Auctions.BusinessUseCases
{
    public class WatchAuction
    {
        public Guid MemberId { get; set; }
        public Guid AuctionId { get; set; }
    }
}
