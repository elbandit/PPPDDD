using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap19.eBidder.Listings.Application.Infrastructure;
using PPPDDDChap19.eBidder.Listings.Model.Auctions.BidHistory;
using PPPDDDChap19.eBidder.Listings.Model.Auctions;
using PPPDDDChap19.eBidder.Listings.Model;
using PPPDDDChap19.eBidder.Listings.Model.QandA;
using PPPDDDChap19.eBidder.Listings.Model.Listings;

namespace PPPDDDChap19.eBidder.Listings.Application.QandA.BusinessUseCases
{
    public class AskAQuestionService
    {
        private IListingRepository _listings;              
        private IQuestionRepository _questions;
        //private IDocumentSession _unitOfWork;
        private IClock _clock;

        public AskAQuestionService(IListingRepository listings,                                   
                                   IQuestionRepository questions,              
                                   IClock clock)
        {
            _listings = listings;
            _questions = questions;
            //_unitOfWork = unitOfWork;
            _clock = clock;
        }

        public void Ask(Guid listingId, Guid memberId, string question)
        {
            var listing = _listings.FindBy(listingId);
          
            using (DomainEvents.Register(QuestionSubmitted()))
            {
                var aQuestion = listing.AskQuestion(memberId, question,  _clock.Time());

                _questions.Add(aQuestion);
            }
        }

        private Action<QuestionSubmitted> QuestionSubmitted()
        {
            return (QuestionSubmitted e) =>
            {
                // Email seller about the question being asked
            };
        }
    }
}
