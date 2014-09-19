using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap19.eBidder.Listings.Application.Infrastructure;

namespace PPPDDDChap19.eBidder.Listings.Model.QandA
{
    public class Question
    {
        private Guid Id { get; set; }
        private Guid SellerId { get; set; }
        private Guid ListingId { get; set; }
        private Guid MemberId { get; set; }
        private bool PublishOnListing { get; set; }
        private DateTime TimeOfQuestion { get; set; }

        public Question(Guid id, Guid listingId, Guid memberId, string question, DateTime timeOfQuestion)
        {
            Id = id;
            ListingId = listingId;
            MemberId = memberId;
            Desc = question;
            TimeOfQuestion = timeOfQuestion;

            DomainEvents.Raise(new QuestionSubmitted(Id, ListingId));
        }

        public string Desc { get; set; }

        public void SubmitAnAnswer(string answer, Guid sellerId, bool publishOnListing, DateTime timeOfAnswer)
        {
            PublishOnListing = publishOnListing;

            Answer = new Answer(timeOfAnswer, answer);

            DomainEvents.Raise(new QuestionAnswered(Id, ListingId)); 
        }

        public Answer Answer { get; private set; }
    }
}
