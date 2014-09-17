using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.Auction.Application.Model.Members.Watching
{
    public interface IWatchedItemRepository
    {
        WatchedItem FindBy(Guid id);
        void Add(WatchedItem watchedItem);
        void Remove(WatchedItem watchedItem);
    }
}
