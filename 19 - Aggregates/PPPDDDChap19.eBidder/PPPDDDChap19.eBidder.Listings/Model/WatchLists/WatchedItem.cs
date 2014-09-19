using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Listings.Model.WatchLists
{
    public class WatchedItem
    {
        public WatchedItem(Guid id, Guid listingId, Guid memberId)
        { 
        
        }

        public Guid Id { get; private set; }
        public Guid ListingId { get; private set; }
        public Guid MemberId { get; private set; }

        private string Note { get; set; }
    }
}
