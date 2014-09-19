using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Listings.Model.WatchLists
{
    public interface IWatchedItemRepository
    {
        WatchedItem FindBy(Guid id);
        void Add(WatchedItem watched);
        void Remove(WatchedItem watched);
    }
}
