using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap19.eBidder.Store.Application.Infrastructure;
using PPPDDDChap19.eBidder.Store.Application.Model.Auctions.BidHistory;
using PPPDDDChap19.eBidder.Store.Application.Model.Auctions;
using PPPDDDChap19.eBidder.Store.Application.Model;
using PPPDDDChap19.eBidder.Store.Application.Model.Members;
using PPPDDDChap19.eBidder.Store.Application.Model.Auctions.QandA;

namespace PPPDDDChap19.eBidder.Store.Application.Auctions.BusinessUseCases
{
    public class AnswerAQuestionService
    {
        private IAuctionRepository _auctions;
        private IMemberRepository _members;
        private IQuestionRepository _questions;
        //private IDocumentSession _unitOfWork;
        private IClock _clock;

        public AnswerAQuestionService(IQuestionRepository questions)
        {
            _questions = questions;
        }
        
        public void Answer(Guid questionId, Guid sellerId, string answer)
        {
            var question = _questions.FindBy(questionId);

            // TODO:Publish to item so that all can view

            question.SubmitAnAnswer(answer, sellerId);
        }

    }
}
