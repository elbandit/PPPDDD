using System;
using DDDPPP.Chap19.NHibernateExample.Application.Model.Auction;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using DDDPPP.Chap19.NHibernateExample.Application.Application;

namespace DDDPPP.Chap19.NHibernateExample.Application.Infrastructure
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly ISession _session;

        public AuctionRepository(ISession session)
        { 
            _session = session;
        }

        public void Add(Auction auction)
        {
            _session.Save(auction); 
        }

        public Auction FindBy(Guid Id)
        {
            return _session.Get<Auction>(Id);
        }
    }
}
