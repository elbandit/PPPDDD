using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDPPP.Chap19.MicroORM.Application.Model.BidHistory
{
    public class BidHistory
    {
        private IEnumerable<Bid> _bids;

        public BidHistory(IEnumerable<Bid> bids)
        {
            if (bids == null)
                throw new ArgumentNullException("Bids cannot be null");

            _bids = bids;
        }

        public IEnumerable<Bid> ShowAllBids()
        {
            var bids = _bids.OrderByDescending(x => x.AmountBid).ThenBy(x => x.TimeOfBid);

            return bids;
        }
    }
}