using System;
using System.Collections.Generic;
using DDDPPP.Chap19.EFExample.Application.Model.Auction;
using DDDPPP.Chap19.EFExample.Application.Model.BidHistory;
using DDDPPP.Chap19.EFExample.Application.Infrastructure;
using System.Data.Entity.Infrastructure;

namespace DDDPPP.Chap19.EFExample.Application.Application.BusinessUseCases
{
    public class BidOnAuction
    {
        private IAuctionRepository _auctions;
        private IBidHistoryRepository _bidHistory;
        private AuctionDatabaseContext _unitOfWork;
        private IClock _clock;

        public BidOnAuction(IAuctionRepository auctions, IBidHistoryRepository bidHistory, AuctionDatabaseContext unitOfWork, IClock clock)
        {
            _auctions = auctions;
            _bidHistory = bidHistory;
            _unitOfWork = unitOfWork;
            _clock = clock;
        }

        public void Bid(Guid auctionId, Guid memberId, decimal amount)
        {
            try
            {
                using (DomainEvents.Register(OutBid()))
                using (DomainEvents.Register(BidPlaced()))
                {
                    var auction = _auctions.FindBy(auctionId);

                    var bidAmount = new Money(amount);

                    auction.PlaceBidFor(new Offer(memberId, bidAmount, _clock.Time()), _clock.Time());

                    _auctions.Save(auction);
                }

                _unitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // What happens if the auction is changed after we retrieve it and before we save it?               
                // try again with the updated auction
                _unitOfWork.Clear();

                Bid(auctionId, memberId, amount);
            }            
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
