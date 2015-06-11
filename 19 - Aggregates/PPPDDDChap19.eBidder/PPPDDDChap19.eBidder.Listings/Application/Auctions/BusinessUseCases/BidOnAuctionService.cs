using System;
using PPPDDDChap19.eBidder.Listings.Application.Infrastructure;
using PPPDDDChap19.eBidder.Listings.Model.Auctions.BidHistory;
using PPPDDDChap19.eBidder.Listings.Model.Auctions;
using PPPDDDChap19.eBidder.Listings.Model;
using PPPDDDChap19.eBidder.Listings.Model.Members;

namespace PPPDDDChap19.eBidder.Listings.Application.Application.BusinessUseCases
{
    public class BidOnAuctionService
    {
        private IAuctionRepository _auctions;
        private IBidHistoryRepository _bidHistory;
        //private IDocumentSession _unitOfWork;
        private IClock _clock;
        private IMemberService _memberService;

        public BidOnAuctionService(IAuctionRepository auctions,
                                   IBidHistoryRepository bidHistory, 
                                   IMemberService memberService,
                                   IClock clock)
        {
            _auctions = auctions;
            _bidHistory = bidHistory;
            _clock = clock;
            _memberService = memberService;
        }

        public void Bid(Guid auctionId, Guid memberId, decimal amount)
        {            
            using (DomainEvents.Register(OutBid()))
            using (DomainEvents.Register(BidPlaced()))
            {                    
                var member = _memberService.GetMember(memberId);

                if (member.CanBid)
                { 
                    var auction = _auctions.FindBy(auctionId);

                    var bidAmount = new Money(amount);

                    var offer = new Offer(memberId, bidAmount, _clock.Time());

                    auction.PlaceBidFor(offer, _clock.Time());
                }                    
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
                // Add message to Member message board.
                // 
            };
        }
    }
}
