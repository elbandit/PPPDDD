using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PPPDDDChap05.TransactionScript.Domain
{
    public class BidOnAuctionCommand: ICommand
    {
        private Guid auctionId {get; set;}
        private Guid bidderId {get; set;}
        private decimal amount {get; set;}
        private DateTime timeOfBid { get; set; }

        public BidOnAuctionCommand(Guid auctionId, Guid bidderId, decimal amount, DateTime timeOfBid)
        {
            this.auctionId = auctionId;
            this.bidderId = bidderId;
            this.amount = amount;
            this.timeOfBid = timeOfBid;
        }

        public void Execute()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                ThrowExceptionIfNotValid(auctionId, bidderId, amount, timeOfBid);

                ThrowExceptionIfAuctionHasEnded(auctionId);

                if (IsFirstBid(auctionId))
                    PlaceFirstBid(auctionId, bidderId, amount, timeOfBid);
                else if (IsIncreasingMaximimBid(auctionId, amount, bidderId))
                    IncreaseMaximumBidTo(amount);
                else if (CanMeetOrExceedBidIncrement(amount))
                    UpdatePrice(auctionId, bidderId, amount, timeOfBid);
            }
        }

        private void ThrowExceptionIfAuctionHasEnded(Guid auctionId)
        {
            // check at DB
        }

        private bool CanMeetOrExceedBidIncrement(decimal amount)
        {
            return true;
        }

        private void UpdatePrice(Guid auctionId, Guid bidderId, decimal amount, DateTime timeOfBi)
        {
        
        }

        private void IncreaseMaximumBidTo(decimal amount)
        { 
        
        }

        private bool IsIncreasingMaximimBid(Guid auctionId, decimal amount, Guid bidderId)
        {
            return true;
        }

        private bool IsFirstBid(Guid auctionId)
        {
            return true;
        }

        private void PlaceFirstBid(Guid auctionId, Guid bidderId, decimal amount, DateTime timeOfBid)
        { 
        
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
