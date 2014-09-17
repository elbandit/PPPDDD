using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Listings.Model.QandA
{
    public class Question
    {
        private Guid Id { get; set; }
        private Guid SellerId { get; set; }
        private Guid AuctionId { get; set; }
        private Guid MemberId { get; set; }


        public Question(Guid id, Guid auctionId, Guid memberId, string question)
        { 
        
        }

        public string Desc { get; set; }

        public void SubmitAnAnswer(string answer, Guid sellerId)
        { 
            
        }

        public Answer Answer { get; set; }
    }
}
