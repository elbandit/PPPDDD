using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Listings.Model.Watchers
{
    public interface IWatcherRepository
    {
        Watcher FindBy(Guid id);
        void Add(Watcher watchedItem);
        void Remove(Watcher watchedItem);
    }
}
