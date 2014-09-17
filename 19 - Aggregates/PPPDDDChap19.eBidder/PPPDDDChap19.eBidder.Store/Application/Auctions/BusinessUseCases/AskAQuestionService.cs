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
    public class AskAQuestionService
    {
        private IAuctionRepository _auctions;
        private IMemberRepository _members;
        private IQuestionRepository _questions;
        //private IDocumentSession _unitOfWork;
        private IClock _clock;

        public AskAQuestionService(IAuctionRepository auctions,
                                   IMemberRepository members,  
                                   IQuestionRepository questions,              
                                   IClock clock)
        {
            _auctions = auctions;
            _members = members;
            _questions = questions;
            //_unitOfWork = unitOfWork;
            _clock = clock;
        }

        public void Ask(Guid auctionId, Guid memberId, string question)
        {
            // Ensure member exisits
            var member = _members.FindBy(memberId);

            // Ensure auction exisits
            var auction = _auctions.FindBy(auctionId);

            var aQuestion = auction.AskQuestion(memberId, question);

            _questions.Add(aQuestion);
        }
    }
}
