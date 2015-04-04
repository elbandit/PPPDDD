using System;

namespace DDDPPP.Chap21.NHibernateExample.Application.Application.BusinessUseCases
{
    public class NewAuctionRequest
    {
        public decimal StartingPrice { get; set; }
        public DateTime EndsAt { get; set; }
    }
}
