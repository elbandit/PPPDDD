using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Listings.Model.QandA
{
    public class QuestionSubmitted
    {
        public QuestionSubmitted(Guid questionId, Guid listingId)
        {
            if (questionId == Guid.Empty)
                throw new ArgumentNullException("Question Id cannot be null");

            if (listingId == Guid.Empty)
                throw new ArgumentNullException("Listing Id cannot be null");

            QuestionId = questionId;
            ListingId = listingId;
        }

        public Guid ListingId { get; private set; }
        public Guid QuestionId { get; private set; }
    }
}
