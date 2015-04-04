using System;
using System.Collections.Generic;
using DDDPPP.Chap21.NHibernateExample.Application.Application.BusinessUseCases;
using DDDPPP.Chap21.NHibernateExample.Application.Application.Queries;
using DDDPPP.Chap21.NHibernateExample.Application;
using StructureMap;

namespace DDDPPP.Chap21.NHibernateExample.Presentation
{
    public class Program
    {
       private static Dictionary<Guid, String> members = new Dictionary<Guid, string>();

       public static void Main(string[] args)
        {
            Bootstrapper.Startup();
                     
            var memberIdA = Guid.NewGuid();
            var memberIdB = Guid.NewGuid();

            members.Add(memberIdA, "Ted");
            members.Add(memberIdB, "Rob");

            var auctionId = CreateAuction();
            
            Bid(auctionId, memberIdA, 10m);
            Bid(auctionId, memberIdB, 1.49m);
            Bid(auctionId, memberIdB, 10.01m);
            Bid(auctionId, memberIdB, 12.00m);
            Bid(auctionId, memberIdA, 12.00m);
        }

       public static Guid CreateAuction()
       {
           var createAuctionService = ObjectFactory.GetInstance<CreateAuction>();

           var newAuctionRequest = new NewAuctionRequest();

           newAuctionRequest.StartingPrice = 0.99m;
           newAuctionRequest.EndsAt = DateTime.Now.AddDays(1);

           var auctionId = createAuctionService.Create(newAuctionRequest);

           return auctionId;
       }

       public static void Bid(Guid auctionId, Guid memberId, decimal amount)
       {
           var bidOnAuctionService = ObjectFactory.GetInstance<BidOnAuction>();

           bidOnAuctionService.Bid(auctionId, memberId, amount);

           PrintStatusOfAuctionBy(auctionId);
           PrintBidHistoryOf(auctionId);
           Console.WriteLine("Hit any key to continue");
           Console.ReadLine();
       }

        public static void PrintStatusOfAuctionBy(Guid auctionId)
        {
            var auctionSummaryQuery = ObjectFactory.GetInstance<AuctionStatusQuery>();
            var status = auctionSummaryQuery.AuctionStatus(auctionId);
           
            Console.WriteLine("No Of Bids: " + status.NumberOfBids);
            Console.WriteLine("Current Bid: " + status.CurrentPrice.ToString("##.##"));
            Console.WriteLine("Winning Bidder: " + FindNameOfBidderWith(status.WinningBidderId));
            Console.WriteLine("Time Remaining: " + status.TimeRemaining);            
            Console.WriteLine();
        }

        public static void PrintBidHistoryOf(Guid auctionId)
        {
            var bidHistoryQuery = ObjectFactory.GetInstance<BidHistoryQuery>();
            var status = bidHistoryQuery.BidHistoryFor(auctionId);

            Console.WriteLine("Bids..");

            foreach (var bid in status)
                Console.WriteLine(FindNameOfBidderWith(bid.Bidder) + "\t - " + bid.AmountBid.ToString("G") + "\t at " + bid.TimeOfBid);
            Console.WriteLine("------------------------------");
            Console.WriteLine();
        }

        public static string FindNameOfBidderWith(Guid id)
        {
            if (members.ContainsKey(id))
                return members[id];
            else
                return string.Empty;
        }
    }
}
