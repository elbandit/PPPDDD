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

namespace PPPDDDChap19.eBidder.Listings.Application.QandA.BusinessUseCases
{
    public class AnswerAQuestionService
    {
        private IAuctionRepository _auctions;
        private IQuestionRepository _questions;
        //private IDocumentSession _unitOfWork;
        private IClock _clock;

        public AnswerAQuestionService(IQuestionRepository questions, IClock clock)
        {
            _questions = questions;
            _clock = clock;
        }
        
        public void Answer(Guid questionId, Guid sellerId, string answer, bool publishOnListing)
        {
            var question = _questions.FindBy(questionId);

            using (DomainEvents.Register(QuestionAnswered()))
            {
                question.SubmitAnAnswer(answer, sellerId, publishOnListing, _clock.Time());
            }
        }

        private Action<QuestionAnswered> QuestionAnswered()
        {
            return (QuestionAnswered e) =>
            {
                // Email member about the question being answered
            };
        }

    }
}
