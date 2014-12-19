using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap05.DomainModel.Model
{
    public class HistoricalBid
    {
        public HistoricalBid(Guid bidder, Money Bid, DateTime timeOfBid)
        { 
        
        }


        public Guid Bidder {get; set;} 
        public Money Amount {get; set;}
        public DateTime TimeOfBid { get; set; }
    }
}
