using System;

namespace DDDPPP.Chap21.NHibernateExample.Application.Application.Queries
{
    public class BidInformation
    {
        public Guid Bidder { get;  set; }
        public decimal AmountBid { get;  set; }
        public string currency { get; set; }
        public DateTime TimeOfBid { get;  set; }
    }
}
