using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Store.Application.Model.Auctions.QandA
{
    public class Question
    {
        public Question(Guid id, Guid auctionId, Guid memberId)
        { 
        
        }

        public string Desc { get; set; }

        public void SubmitAnAnswer(string answer)
        { 
            
        }

        public Answer Answer { get; set; }
    }
}
