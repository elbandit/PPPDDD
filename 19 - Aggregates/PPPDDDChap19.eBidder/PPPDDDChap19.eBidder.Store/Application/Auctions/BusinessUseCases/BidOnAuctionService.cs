using System;
using PPPDDDChap19.eBidder.Store.Application.Infrastructure;
using PPPDDDChap19.eBidder.Store.Application.Model.Auctions.BidHistory;
using PPPDDDChap19.eBidder.Store.Application.Model.Auctions;
using PPPDDDChap19.eBidder.Store.Application.Model;
using PPPDDDChap19.eBidder.Store.Application.Model.Members;

namespace PPPDDDChap19.eBidder.Store.Application.Application.BusinessUseCases
{
    public class BidOnAuctionService
    {
        private IAuctionRepository _auctions;
        private IBidHistoryRepository _bidHistory;
        private IMemberRepository _members;
        //private IDocumentSession _unitOfWork;
        private IClock _clock;

        public BidOnAuctionService(IAuctionRepository auctions,
                            IBidHistoryRepository bidHistory, 
                            IMemberRepository members,
                            IClock clock)
        {
            _auctions = auctions;
            _bidHistory = bidHistory;
            _members = members;
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
                    // Ensure member exisits
                    var member = _members.FindBy(memberId);

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
                // Add message to Member message board.
                // 
            };
        }
    }
}