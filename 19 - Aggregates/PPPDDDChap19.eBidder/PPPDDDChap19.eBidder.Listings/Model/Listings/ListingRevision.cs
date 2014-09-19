using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap19.eBidder.Listings.Model.Listings
{
    public class ListingRevision
    {
        private Guid Item { get; set; }
        private DateTime Date { get; set; }
        private String RevisedInformation { get; set; }
    }
}
