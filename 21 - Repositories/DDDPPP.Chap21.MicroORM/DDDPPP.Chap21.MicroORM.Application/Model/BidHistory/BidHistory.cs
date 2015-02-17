using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDPPP.Chap21.MicroORM.Application.Model.BidHistory
{
    public class BidHistory
    {
        private IEnumerable<HistoricalBid> _bids;

        public BidHistory(IEnumerable<HistoricalBid> bids)
        {
            if (bids == null)
                throw new ArgumentNullException("Bids cannot be null");

            _bids = bids;
        }

        public IEnumerable<HistoricalBid> ShowAllBids()
        {
            var bids = _bids.OrderByDescending(x => x.AmountBid).ThenBy(x => x.TimeOfBid);

            return bids;
        }
    }
}