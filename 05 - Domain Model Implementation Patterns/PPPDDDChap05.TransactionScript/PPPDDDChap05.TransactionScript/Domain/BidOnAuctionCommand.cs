using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap05.TransactionScript.Domain
{
    public class BidOnAuctionCommand
    {
        public BidOnAuctionCommand()
        { }

        public void Execute(Guid auctionId, Guid bidderId, decimal amount, DateTime dateOfBid)
        {
            ThrowExceptionIfNotValid(auctionId, bidderId, amount, dateOfBid);

            Auction auction = new Auction();

            ThrowExceptionIfAuctionHasEnded(auction, dateOfBid);

            if (auction.WinningBidder == Guid.Empty)
            {
                FirstBid(auction, bidderId, amount, dateOfBid);
            }
            else
            {
                if (auction.WinningBidder == bidderId && amount > auction.WinninBidderMaximumBid)
                {
                    auction.WinninBidderMaximumBid = amount;
                }
                else
                { 
                    if (amount >= (auction.WinningBid + BidIncrement(auction.WinningBid)))
                    {                        
                        if (amount <= auction.WinninBidderMaximumBid)
                        { 
                           if (auction.WinninBidderMaximumBid >= (auction.WinningBid + BidIncrement(auction.WinningBid))
                               auction.WinningBid = (auction.WinningBid + BidIncrement(auction.WinningBid);
                           else
                               auction.WinningBid = auction.WinninBidderMaximumBid;
                        }
                        else
                        {
                        
                        }
                    }
                    else
                        throw new ApplicationException(String.Format("You need to bid at least {0}",auction.WinningBid + BidIncrement(auction.WinningBid)));
                }
            }            
        }

        private void ThrowExceptionIfAuctionHasEnded(Auction auction, DateTime dateOfBid)
        {
            if (auction.EndsAt < dateOfBid)
            {
                throw new ApplicationException("Auction has already ended");
            }
        }

        private void FirstBid(Auction auction, Guid bidderId, decimal amount, DateTime dateOfBid)
        {
            if (amount > auction.StartingPrice)
            {
                auction.WinningBidder = bidderId;
                auction.WinningBid = amount;

                // DB Update
            }
            else
                throw new ApplicationException("You have to bid greater than the starting price.");
        }

        private void ThrowExceptionIfNotValid(Guid auctionId, Guid bidderId, decimal amount, DateTime dateOfBid)
        { 
            if (auctionId == Guid.Empty)
                throw new ArgumentNullException("AuctionId cannot be null");

            if (bidderId == Guid.Empty)
                throw new ArgumentNullException("BidderId cannot be null");

            if (dateOfBid == DateTime.MinValue)
                throw new ArgumentNullException("Time of bid must have a value");

            if (amount % 0.01m != 0)
                throw new InvalidOperationException("There cannot be more than two decimal places.");

            if (amount < 0)
                throw new InvalidOperationException("Money cannot be a negative value.");
        }

        private decimal BidIncrement(decimal currentAuctionWinningBid)
        {
            if (currentAuctionWinningBid >= 0.01m && currentAuctionWinningBid <= 0.99m)
                return 0.05m;

            if (currentAuctionWinningBid >= 1.00m  && currentAuctionWinningBid <= 4.99m)
                return 0.20m;

            if (currentAuctionWinningBid >= 5.00m && currentAuctionWinningBid >= 14.99m)
                return 0.50m;

            return 1.00m;

        }

    }
}
