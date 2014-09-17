using System;
using PPPDDDChap23.Auction.Application.Infrastructure;
using PPPDDDChap23.Auction.Application.Model.Auctions.BidHistory;
using PPPDDDChap23.Auction.Application.Model.Auctions;
using PPPDDDChap23.Auction.Application.Model;

namespace PPPDDDChap23.Auction.Application.Application.BusinessUseCases
{
    public class BidOnAuctionService
    {
        private IAuctionRepository _auctions;
        private IBidHistoryRepository _bidHistory;
        //private IDocumentSession _unitOfWork;
        private IClock _clock;

        public BidOnAuctionService(IAuctionRepository auctions,
                            IBidHistoryRepository bidHistory,                            
                            IClock clock)
        {
            _auctions = auctions;
            _bidHistory = bidHistory;
            //_unitOfWork = unitOfWork;
            _clock = clock;
        }

        public void Bid(Guid auctionId, Guid memberId, decimal amount)
        {
            //try
            {
                using (DomainEvents.Register(OutBid()))
                using (DomainEvents.Register(BidPlaced()))
                {
                    var auction = _auctions.FindBy(auctionId);

                    var bidAmount = new Money(amount);

                    auction.PlaceBidFor(new Offer(memberId, bidAmount, _clock.Time()), _clock.Time());
                }

                //_unitOfWork.SaveChanges();
            }
            //catch (ConcurrencyException ex)
            //{
            //    //_unitOfWork.Advanced.Clear();
            //    Bid(auctionId, memberId, amount);
            //}
        }

        private Action<BidPlaced> BidPlaced()
        {
            return (BidPlaced e) =>
            {
                var bidEvent = new Bid(e.AuctionId, e.Bidder, e.AmountBid, e.TimeOfBid);

                _bidHistory.Add(bidEvent);
            };
        }

        private Action<OutBid> OutBid()
        {
            return (OutBid e) =>
            {
                // Email customer to say that he has been out bid                
            };
        }
    }
}