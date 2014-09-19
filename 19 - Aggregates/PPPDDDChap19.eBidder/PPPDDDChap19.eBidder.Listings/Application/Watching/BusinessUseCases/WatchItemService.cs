using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap19.eBidder.Listings.Model.Auctions;
using PPPDDDChap19.eBidder.Listings.Model.WatchLists;
using PPPDDDChap19.eBidder.Listings.Model.Listings;

namespace PPPDDDChap19.eBidder.Listings.Application.Watching.BusinessUseCases
{
    public class WatchItemService
    {
        private IListingRepository _listings;       
        private IWatchedItemRepository _watchedItems;
        // private IDocumentSession _unitOfWork;

        public WatchItemService(IListingRepository listings, IWatchedItemRepository watchedItems)
        {            
            _watchedItems = watchedItems;
            _listings = listings;
            // _unitOfWork = unitOfWork;
        }

        public void Watch(WatchItem command)
        {
            // Ensure Auction exisits
            var item = _listings.FindBy(command.AuctionId);
            
            var watch = item.Watch(Guid.NewGuid(), command.MemberId);

            _watchedItems.Add(watch); // DB will enforce unique contraint on no member watching more than a single item
                     
            // _unitOfWork.SaveChanges();
        }

        public void UnWatch(UnWatchItem command)
        {
            var watchedItem = _watchedItems.FindBy(command.WatchedItemId);

            _watchedItems.Remove(watchedItem);
        }
    }
}
