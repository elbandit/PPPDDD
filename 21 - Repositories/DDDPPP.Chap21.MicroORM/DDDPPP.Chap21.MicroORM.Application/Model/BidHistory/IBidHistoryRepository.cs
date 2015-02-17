using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPPP.Chap21.MicroORM.Application.Model.BidHistory
{
    public interface IBidHistoryRepository
    {
        int NoOfBidsFor(Guid autionId);
        void Add(HistoricalBid bid);
        BidHistory FindBy(Guid auctionId);
    }
}
