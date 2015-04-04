using System;
using DDDPPP.Chap21.MicroORM.Application.Model.Auction;
using DDDPPP.Chap21.MicroORM.Application.Infrastructure;

namespace DDDPPP.Chap21.MicroORM.Application.Application.BusinessUseCases
{
    public class CreateAuction
    {
        private IAuctionRepository _auctionRepository;
        private IUnitOfWork _unitOfWork;

        public CreateAuction(IAuctionRepository auctionRepository, IUnitOfWork unitOfWork)
        {
            _auctionRepository = auctionRepository;            
            _unitOfWork = unitOfWork;
        }

        public Guid Create(NewAuctionRequest command)
        {
            var auctionId = Guid.NewGuid();
            var startingPrice = new Money(command.StartingPrice);

            _auctionRepository.Add(new Auction(auctionId, startingPrice, command.EndsAt));

            _unitOfWork.Commit();
            
            return auctionId;
        }
    }
}
