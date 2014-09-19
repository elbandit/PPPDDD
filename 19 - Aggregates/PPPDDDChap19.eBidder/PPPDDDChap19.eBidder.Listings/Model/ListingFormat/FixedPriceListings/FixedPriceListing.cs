using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPPDDDChap19.eBidder.Listings.Application.Infrastructure;
using PPPDDDChap19.eBidder.Listings.Model.QandA;
using PPPDDDChap19.eBidder.Listings.Model.Listings;

namespace PPPDDDChap19.eBidder.Listings.Model.FixedPrice
{
    public class FixedPriceListing
    {
        private FixedPriceListing() { }

        public FixedPriceListing(Guid id, Guid sellerId, Money buyNowPrice, DateTime endsAt, Listing listing)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("Auction Id cannot be null");

            if (sellerId == Guid.Empty)
                throw new ArgumentNullException("Seller Id cannot be null");

            if (buyNowPrice == null)
                throw new ArgumentNullException("The buy it now price cannot be null");

            if (endsAt == DateTime.MinValue)
                throw new ArgumentNullException("EndsAt must have a value");

            if (listing == null)
                throw new ArgumentNullException("The Item cannot be null");

            Id = id;
            BuyNowPrice = buyNowPrice;
            EndsAt = endsAt;
            Listing = listing;
        }

        private Guid Id { get; set; }
        private Guid sellerId { get; set; }
        private Listing Listing { get; set; }
        private DateTime EndsAt { get; set; }
        private Money BuyNowPrice { get; set; }

        public void BestOffer()
        {
            // http://ocsnext.ebay.co.uk/ocs/sc
            // for Buy It Now listing only
        }
    }
}
