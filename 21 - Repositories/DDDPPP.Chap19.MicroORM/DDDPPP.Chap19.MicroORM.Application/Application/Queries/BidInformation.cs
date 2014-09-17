using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPPP.Chap19.MicroORM.Application.Application.Queries
{
    public class BidInformation
    {
        public Guid Bidder { get;  set; }
        public decimal AmountBid { get;  set; }
        public string Currency { get; set; }
        public DateTime TimeOfBid { get;  set; }
    }
}
