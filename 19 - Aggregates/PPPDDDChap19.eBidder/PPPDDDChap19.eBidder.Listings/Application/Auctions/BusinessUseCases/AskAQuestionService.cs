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

namespace PPPDDDChap19.eBidder.Listings.Application.Auctions.BusinessUseCases
{
    public class AskAQuestionService
    {
        private IAuctionRepository _auctions;
        
        private IQuestionRepository _questions;
        //private IDocumentSession _unitOfWork;
        private IClock _clock;

        public AskAQuestionService(IAuctionRepository auctions,                                   
                                   IQuestionRepository questions,              
                                   IClock clock)
        {
            _auctions = auctions;
            _questions = questions;
            //_unitOfWork = unitOfWork;
            _clock = clock;
        }

        public void Ask(Guid auctionId, Guid memberId, string question)
        {           
            // Ensure auction exisits
            var auction = _auctions.FindBy(auctionId);

            var aQuestion = auction.AskQuestion(memberId, question);

            _questions.Add(aQuestion);
        }
    }
}
