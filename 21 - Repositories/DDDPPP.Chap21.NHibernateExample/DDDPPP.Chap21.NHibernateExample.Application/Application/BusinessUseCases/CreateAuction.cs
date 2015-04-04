using System;
using DDDPPP.Chap21.NHibernateExample.Application.Model.Auction;
using NHibernate;

namespace DDDPPP.Chap21.NHibernateExample.Application.Application.BusinessUseCases
{
    public class CreateAuction
    {
        private IAuctionRepository _auctionRepository;
        private ISession _unitOfWork;

        public CreateAuction(IAuctionRepository auctionRepository,ISession unitOfWork)
        {
            _auctionRepository = auctionRepository;            
            _unitOfWork = unitOfWork;
        }

        public Guid Create(NewAuctionRequest command)
        {
            var auctionId = Guid.NewGuid();
            var startingPrice = new Money(command.StartingPrice);

            using (ITransaction transaction = _unitOfWork.BeginTransaction())
            {
                _auctionRepository.Add(new Auction(auctionId, startingPrice, command.EndsAt));

                transaction.Commit();
            }

            return auctionId;
        }
    }
}
