using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Listings.Application.Watching.BusinessUseCases
{
    public class UnWatchItem
    {
        public Guid MemberId { get; set; }
        public Guid WatchedItemId { get; set; }
    }
}
