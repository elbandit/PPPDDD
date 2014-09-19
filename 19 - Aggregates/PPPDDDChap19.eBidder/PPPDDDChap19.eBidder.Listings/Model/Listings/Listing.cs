using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap19.eBidder.Listings.Model.WatchLists;
using PPPDDDChap19.eBidder.Listings.Model.QandA;

namespace PPPDDDChap19.eBidder.Listings.Model.Listings
{
    public class Listing
    {
        public Listing(Guid id, Guid sellerId, ListingFormat format)
        {
            if (sellerId == Guid.Empty)
                throw new ArgumentNullException("Seller Id cannot be null");

            Id = id;
            Format = format;
            SellerId = sellerId;
        }

        private Guid Id { get; set; }
        private ListingFormat Format { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private string Condition { get; set; }
        private Guid SellerId { get; set; }

        private decimal PostageCosts { get; set; }

        private string PaymentMethodsAccepted { get; set; }
        private string DispatchTime { get; set; }

        //public Email ContactSeller(string question)
        //{ 
        // You'll receive the message in your Messages inbox and your personal email account. 
        // When you respond, you can choose to post the question and answer to your listing 
        // so all buyers can see it. Once you post the answer, you can't change or remove it.
        //}

        public WatchedItem Watch(Guid watchedItemId, Guid memberId)
        {
            return new WatchedItem(watchedItemId, this.Id, memberId);
        }

        //public void Add(PaymentMethod paymentMethod)
        //{
        // replace Item and add PaymentMethod       
        //}

        //public void Add(PostLocation()
        //{ 

        //}

        public Question AskQuestion(Guid MemberId, string quesiton, DateTime timeOfQuestion)
        {
            return new Question(Guid.NewGuid(), this.Id, MemberId, quesiton, timeOfQuestion);
        }
       
       // public void Amend(Item item)
       // {
            // http://pages.ebay.co.uk/help/sell/revising_restrictions.html
            // if (currentTime.
            // Throw New ItemRevisionEvent(Description), DateTime currentTime          
      //  }
    }


}
