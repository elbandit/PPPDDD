using System;

namespace PPPDDDChap23.Auction.Application.Application.BusinessUseCases
{
    public class AuctionCreation
    {
        public decimal StartingPrice { get; set; }
        public Guid SellerId { get; set; }
        public DateTime EndsAt { get; set; }
    }
}
