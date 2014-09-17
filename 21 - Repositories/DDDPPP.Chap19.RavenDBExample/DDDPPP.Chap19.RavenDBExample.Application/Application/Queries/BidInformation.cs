using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPPP.Chap19.RavenDBExample.Application.Application.Queries
{
    public class BidInformation
    {
        public Guid Bidder { get;  set; }
        public decimal AmountBid { get;  set; }
        public string currency { get; set; }
        public DateTime TimeOfBid { get;  set; }
    }
}
