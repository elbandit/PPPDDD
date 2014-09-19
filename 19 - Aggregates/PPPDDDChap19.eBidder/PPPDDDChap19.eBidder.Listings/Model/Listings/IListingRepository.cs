using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Listings.Model.Listings
{
    public interface IListingRepository
    {
        void Add(Listing listing);
        Listing FindBy(Guid id);
    }
}
