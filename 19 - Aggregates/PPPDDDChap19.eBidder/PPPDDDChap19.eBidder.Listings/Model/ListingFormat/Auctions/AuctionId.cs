using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Listings.Model.ListingFormat.Auctions
{
    public class AuctionId
    {
        public AuctionId(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
