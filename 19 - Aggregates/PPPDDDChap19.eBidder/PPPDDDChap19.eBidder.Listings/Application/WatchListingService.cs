using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap19.eBidder.Listings.Model.Auctions;
using PPPDDDChap19.eBidder.Listings.Model.Watchers;

namespace PPPDDDChap19.eBidder.Listings.Application.Application.Auctions
{
    public class WatchListingService
    {
        private IAuctionRepository _auctions;       
        private IWatcherRepository _watchedItems;
        // private IDocumentSession _unitOfWork;

        public WatchListingService(IAuctionRepository auctions, IWatcherRepository watchedItems)
        {
            _auctions = auctions;
            _watchedItems = watchedItems;
            // _unitOfWork = unitOfWork;
        }

        public void Watch(WatchListing command)
        {
            // Ensure Auction exisits
            var auction = _auctions.FindBy(command.AuctionId);
            
            var watchItem = auction.Watch(command.MemberId);

            _watchedItems.Add(watchItem);
          
            // _unitOfWork.SaveChanges();
        }
    }
}
