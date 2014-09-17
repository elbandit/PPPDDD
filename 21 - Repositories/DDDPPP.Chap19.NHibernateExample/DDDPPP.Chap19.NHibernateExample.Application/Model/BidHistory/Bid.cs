using System;
using System.Collections.Generic;
using DDDPPP.Chap19.NHibernateExample.Application.Model.Auction;
using DDDPPP.Chap19.NHibernateExample.Application.Infrastructure;

namespace DDDPPP.Chap19.NHibernateExample.Application.Model.BidHistory
{
    public class Bid : ValueObject<Bid>
    {
        private Bid()
        { }

        public Bid(Guid auctionId, Guid bidderId, Money amountBid, DateTime timeOfBid)
        {
            if (auctionId == Guid.Empty)
                throw new ArgumentNullException("Auction Id cannot be null");

            if (bidderId == Guid.Empty)
                throw new ArgumentNullException("Bidder Id cannot be null");

            if (amountBid == null)
                throw new ArgumentNullException("AmountBid cannot be null");

            if (timeOfBid == DateTime.MinValue)
                throw new ArgumentNullException("TimeOfBid must have a value");

            AuctionId = auctionId;
            Bidder = bidderId;
            AmountBid = amountBid;
            TimeOfBid = timeOfBid;
        }

        public Guid AuctionId { get; private set; }
        public Guid Bidder { get; private set; }
        public Money AmountBid {get; private set;}
        public DateTime TimeOfBid { get; private set; }
        private Guid Id { get; set; }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object>() { Bidder, AuctionId, TimeOfBid, AmountBid };
        }
    }
}
